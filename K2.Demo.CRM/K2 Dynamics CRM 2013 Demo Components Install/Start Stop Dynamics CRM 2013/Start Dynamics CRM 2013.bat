net start MSCRMAsyncService
net start MSCRMAsyncService$maintenance
REM net start MSCRMSandboxService
REM net start MSCRMUnzipService
net start MSCRMMonitoringService

SET CURRENTDIR=%CD%

PowerShell.exe -NoProfile -file "%CURRENTDIR%\startCRMAppPool.ps1" -path "%CURRENTDIR%"