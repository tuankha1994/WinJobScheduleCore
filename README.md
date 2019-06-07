# WinJobScheduleCore
Windows services with Quartz perform many scheduled jobs. Application implemented on the .NET Core.

#Build package
cmd: dotnet publish --configuration Release

#Deploy
cmd: sc create <ServiceName> binPath= "<BinaryPathName>"
ex: sc create serviceName binpath= "D:/publish/WinJobScheduleCore.exe"

#Start/Stop service
cmd: sc start/stop MyService

#Delete service
cmd: sc delete MyService
