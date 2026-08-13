# SuzerainReasonAboutReforms

`SuzerainReasonAboutReforms` is a MelonLoader and Suzerain Modding Kit mod for
recovering selected failed negotiations with Gloria Tory, Albin Clavin, and
Isabel Edmonds during the constitutional reform campaign.

Version `1.0.1` retains the eight in-game-validated Gloria recoveries, the three
Albin recoveries, and adds an Isabel outcome redirect with one short narrator
transition. It targets the selector constructor signatures introduced by SMK
`2.4`.

## Added conversation behavior

When enabled, the mod registers one SMK conversations for:

```text
Sordland/Turn04/A_ConvinceGloria
```

The conversations add recovery arguments at seven constitutional-reform rejection
points and after refusing Gloria's request to cancel Monica's Benfi speech.
Persuasive replies return to the documented vanilla success continuations;
hostile or uncompromising replies retain the vanilla Gloria-failure outcome.
The mod does not independently rewrite a reform clause or add save variables.

When enabled, the mod also registers one SMK conversations for:

```text
Sordland/Turn04/A_ConvinceAlbin
```

It adds recovery exchanges after ending the negotiation, refusing Albin's
vice-presidential demand, and abandoning his cheque negotiation. Each exchange
offers two political arguments, a clear option to end the negotiation in
failure, and a final sharper threat that makes Albin cave. Successful replies
use the vanilla convinced state and exit sequence.

The mod also registers a single redirect in:

```text
Sordland/Turn07/A_ConvinceIsabel
```

The conversation and its choices remain unchanged. If its one final opinion
check selects the failed journal path, the mod shows one short narrator line
and redirects that result to the vanilla convinced journal path. Isabel then
gives her existing support line and the game runs its normal convinced closing
sequence.

## Minimum supported environment

- Windows x64.
- Steam Suzerain `3.1.0.1.175` or newer.
- MelonLoader `0.7.3` or newer, using its .NET 6 runtime.
- Suzerain Modding Kit (SMK) `2.4` or newer.

SMK is a beta dependency and may make breaking changes.

## Installation

For installation:

1. Install and run Suzerain once with MelonLoader.
2. Install SMK as `Suzerain/Mods/SuzerainModdingKit.dll`.
3. Close Suzerain.
4. Place `SuzerainReasonAboutReforms.dll` in `Suzerain/Mods`.

On startup, the MelonLoader log should identify version `1.0.1`, the targeted
SMK version, each configured feature state, and whether each enabled injection
registered successfully.

## Configuration

After the first launch, MelonLoader stores the setting under
`[SuzerainReasonAboutReforms]` in:

```text
Suzerain/UserData/MelonPreferences.cfg
```

Close the game before editing the file.

| Setting | Default | Meaning |
| --- | --- | --- |
| `GloriaEnabled` | `true` | Register the Gloria conversation additions during startup. |
| `AlbinEnabled` | `true` | Register the Albin conversation additions during startup. |
| `IsabelEnabled` | `true` | Always take Isabel's vanilla convinced outcome. |

Each setting independently skips only its named conversation when `false`.
Preferences are read only during startup; close and restart the game after
changing them.

## Building

1. Copy `Directory.Build.props.example` to `Directory.Build.props`.
2. Set `GamePath` to the local Suzerain installation.
3. Leave `DeployMod=false` unless deployment has been explicitly approved.
4. Run:

   ```powershell
   dotnet restore .\SuzerainReasonAboutReforms.sln -p:Platform=x64
   dotnet build .\SuzerainReasonAboutReforms.sln -c Debug -p:Platform=x64
   ```

Create the managed-only Release archive with:

```powershell
.\packaging\Build-Release.ps1
```

The script verifies formatting, performs a clean Release x64 build, and checks
the staged and archived files against `packaging/release-allowlist.txt`.

## Logs and troubleshooting

MelonLoader writes session logs under `Suzerain/MelonLoader/Logs`.

- `Set 'GamePath'...`: create the ignored `Directory.Build.props` and set the
  Suzerain installation path.
- `No Suzerain installation was found`: confirm that `Suzerain.exe` exists
  directly under `GamePath`.
- `MelonLoader.dll was not found`: run Suzerain once after installing
  MelonLoader and verify its .NET 6 files.
- `SuzerainModdingKit.dll was not found`: install SMK in `Suzerain/Mods`.
- Preference initialization error: no conversation additions are registered;
  Suzerain continues running.
- Gloria registration error: inspect the exception in the MelonLoader log and
  confirm the installed game and SMK versions. The mod catches the failure so
  Suzerain can continue.
- Albin registration error: inspect the exception in the MelonLoader log. Its
  registration failure is contained independently from Gloria.
- Isabel registration error: inspect the exception in the MelonLoader log. Its
  registration failure is contained independently from Gloria and Albin.

## Uninstall and rollback

Close Suzerain and remove only:

```text
Suzerain/Mods/SuzerainReasonAboutReforms.dll
```

Optionally remove the `[SuzerainReasonAboutReforms]` section from
`Suzerain/UserData/MelonPreferences.cfg`. Do not remove SMK if another mod uses
it. The mod does not read or write save files directly and registers no custom
save variables.
