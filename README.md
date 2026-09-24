# Shutdowntor

Windows tray app that schedules a local **shutdown** or **reboot**.

- Target: .NET Framework 4.7.2 (WinForms)
- IDE: Visual Studio 2019+
- One instance only; a second launch focuses the running window

## Usage

Open the app, pick a date/time and **Shutdown** or **Reboot**, then Start. The remaining time is shown on the form and the tray icon.

Command-line:

```text
/debug
/hide
/auto:s
/auto:r
/datetime:yyyyMMddHHmmss
```

| Argument | Meaning |
|---|---|
| `/auto:s` `/auto:sd` `/auto:shutdown` | Start countdown as shutdown |
| `/auto:r` `/auto:rb` `/auto:reboot` | Start countdown as reboot |
| `/datetime:20260924180000` | Target local time (`yyyyMMddHHmmss`) |
| `/hide` | Start in the tray |
| `/debug` | Write a debug log |

Example:

```text
Shutdowntor.exe /hide /auto:s /datetime:20260924180000
```

## Sleep and resume

The deadline uses **local wall-clock time**, not a tick countdown.

Windows Forms timers do not fire while the PC is asleep. After this update, resume from sleep re-checks `DateTime.Now` against the target. If the target already passed, the scheduled shutdown or reboot runs then.

## Build

```bash
msbuild Shutdowntor.sln /restore /p:Configuration=Release
```

## Tests and CI

Tests cover argument parsing, action mapping, and deadline math. They do **not** execute shutdown or reboot.

```bash
dotnet test Shutdowntor.Tests/Shutdowntor.Tests.csproj --configuration Release
```

GitHub Actions (`.github/workflows/ci.yml`) runs on `windows-latest`.

## Layout

| Path | Role |
|---|---|
| `Shutdowntor/Program.cs` | Entry, single-instance mutex, CLI |
| `Shutdowntor/Main.cs` | UI, timer, power-resume check |
| `Shutdowntor/Common/StartupOptions.cs` | CLI parse |
| `Shutdowntor/Common/ScheduleClock.cs` | Wall-clock deadline |
| `Shutdowntor/Command/` | Shutdown / reboot commands |
| `Shutdowntor.Tests/` | Unit tests |
