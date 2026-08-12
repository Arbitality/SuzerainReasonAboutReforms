[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Invoke-CheckedCommand {
    param(
        [Parameter(Mandatory)]
        [string] $FilePath,

        [Parameter(Mandatory)]
        [AllowEmptyCollection()]
        [string[]] $CommandArguments
    )

    & $FilePath @CommandArguments
    if ($LASTEXITCODE -ne 0) {
        throw "Command '$FilePath' failed with exit code $LASTEXITCODE."
    }
}

function Get-RelativeFiles {
    param(
        [Parameter(Mandatory)]
        [string] $RootPath
    )

    return Get-ChildItem -LiteralPath $RootPath -File -Recurse |
        ForEach-Object {
            $_.FullName.Substring($RootPath.Length + 1).Replace('\', '/')
        } |
        Sort-Object
}

function Assert-AllowlistMatch {
    param(
        [Parameter(Mandatory)]
        [string[]] $ExpectedFiles,

        [Parameter(Mandatory)]
        [string[]] $ActualFiles,

        [Parameter(Mandatory)]
        [string] $Description
    )

    $differences = Compare-Object `
        -ReferenceObject $ExpectedFiles `
        -DifferenceObject $ActualFiles
    if ($null -ne $differences) {
        $details = $differences | Out-String
        throw "$Description does not match the release allowlist:`n$details"
    }
}

$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$solutionPath = Join-Path $repositoryRoot 'SuzerainReasonAboutReforms.sln'
$projectPath = Join-Path $repositoryRoot `
    'SuzerainReasonAboutReforms\SuzerainReasonAboutReforms.csproj'
$allowlistPath = Join-Path $PSScriptRoot 'release-allowlist.txt'
$releaseRoot = Join-Path $repositoryRoot 'artifacts\release'
$stageRoot = Join-Path $releaseRoot 'SuzerainReasonAboutReforms'

$project = [xml](Get-Content -Raw -LiteralPath $projectPath)
$version = [string]$project.Project.PropertyGroup.Version
if ([string]::IsNullOrWhiteSpace($version)) {
    throw 'Could not read the release version from SuzerainReasonAboutReforms.csproj.'
}

Push-Location $repositoryRoot
try {
    Invoke-CheckedCommand 'dotnet' @(
        'clean',
        $solutionPath,
        '--configuration',
        'Release',
        '--property:Platform=x64'
    )
    Invoke-CheckedCommand 'dotnet' @(
        'restore',
        $solutionPath,
        '--property:Platform=x64'
    )
    Invoke-CheckedCommand 'dotnet' @(
        'format',
        $solutionPath,
        '--verify-no-changes',
        '--no-restore'
    )
    Invoke-CheckedCommand 'dotnet' @(
        'build',
        $solutionPath,
        '--configuration',
        'Release',
        '--property:Platform=x64',
        '--no-restore'
    )
}
finally {
    Pop-Location
}

$releaseRootFull = [System.IO.Path]::GetFullPath($releaseRoot)
$stageRootFull = [System.IO.Path]::GetFullPath($stageRoot)
$requiredPrefix = $releaseRootFull.TrimEnd([System.IO.Path]::DirectorySeparatorChar) +
    [System.IO.Path]::DirectorySeparatorChar
if (!$stageRootFull.StartsWith($requiredPrefix, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Staging path '$stageRootFull' is outside '$releaseRootFull'."
}

if (Test-Path -LiteralPath $stageRootFull) {
    Remove-Item -LiteralPath $stageRootFull -Recurse -Force
}

$null = New-Item -ItemType Directory -Path (Join-Path $stageRootFull 'Mods') -Force

Copy-Item -LiteralPath (
    Join-Path $repositoryRoot `
        'SuzerainReasonAboutReforms\bin\x64\Release\net6.0\SuzerainReasonAboutReforms.dll'
) -Destination (Join-Path $stageRootFull 'Mods\SuzerainReasonAboutReforms.dll')
Copy-Item -LiteralPath (Join-Path $repositoryRoot 'README.md') `
    -Destination (Join-Path $stageRootFull 'README.md')
Copy-Item -LiteralPath (Join-Path $repositoryRoot 'LICENSE') `
    -Destination (Join-Path $stageRootFull 'LICENSE')

$expectedFiles = Get-Content -LiteralPath $allowlistPath |
    Where-Object { ![string]::IsNullOrWhiteSpace($_) } |
    Sort-Object
$actualStagedFiles = Get-RelativeFiles -RootPath $stageRootFull
Assert-AllowlistMatch `
    -ExpectedFiles $expectedFiles `
    -ActualFiles $actualStagedFiles `
    -Description 'Release staging'

$archivePath = Join-Path $releaseRoot `
    "SuzerainReasonAboutReforms-$version-win-x64.zip"
if (Test-Path -LiteralPath $archivePath) {
    Remove-Item -LiteralPath $archivePath -Force
}

Compress-Archive -Path (Join-Path $stageRootFull '*') `
    -DestinationPath $archivePath `
    -CompressionLevel Optimal

$archive = [System.IO.Compression.ZipFile]::OpenRead($archivePath)
try {
    $actualArchivedFiles = @($archive.Entries |
        Where-Object { ![string]::IsNullOrEmpty($_.Name) } |
        ForEach-Object { $_.FullName.Replace('\', '/') } |
        Sort-Object)
}
finally {
    $archive.Dispose()
}

Assert-AllowlistMatch `
    -ExpectedFiles $expectedFiles `
    -ActualFiles $actualArchivedFiles `
    -Description 'Release archive'

Write-Output "Created release archive: $archivePath"
Write-Output 'Staging and archive contents match packaging/release-allowlist.txt.'
