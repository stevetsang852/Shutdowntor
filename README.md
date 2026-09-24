# Shutdowntor

Windows tray app that schedules a local **shutdown** or **reboot**.

IDE: Visual Studio 2019+  
Target: .NET Framework 4.7.2 (WinForms)

## Arguments

```text
/debug
/hide
/auto:s
/auto:r
/datetime:yyyyMMddHHmmss
```

- `/auto:s` or `/auto:shutdown` — start countdown as shutdown
- `/auto:r` or `/auto:reboot` — start countdown as reboot
- `/datetime:20260924180000` — target local time
- `/hide` — start in the tray
- `/debug` — write debug log

Example:

```text
Shutdowntor.exe /hide /auto:s /datetime:20260924180000
```

Only one instance can run. A second launch focuses the existing window.

The deadline uses local wall-clock time. If Windows sleeps past the target, the action runs after resume instead of waiting for leftover timer ticks.

## Tests and CI

Unit tests cover argument parsing, action mapping, and deadline math. They do **not** execute shutdown or reboot.

```bash
dotnet test Shutdowntor.Tests/Shutdowntor.Tests.csproj --configuration Release
```

GitHub Actions (`.github/workflows/ci.yml`) builds the solution on `windows-latest` and runs those tests.
