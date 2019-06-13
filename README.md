# WinJobScheduleCore
Windows services with Quartz perform many scheduled jobs. Application implemented on the .NET Core.

## Build package
```bash
cmd: dotnet publish --configuration Release
```
## Deploy
```bash
cmd: sc create <ServiceName> binPath= "<BinaryPathName>"
```
```bash
ex: sc create serviceName binpath= "D:/publish/WinJobScheduleCore.exe"
```

## Start/Stop service
```bash
cmd: sc start/stop MyService
```
## Delete service
```bash
cmd: sc delete MyService
```
