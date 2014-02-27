net stop MSCRMAsyncService
net stop MSCRMAsyncService$maintenance
REM net stop MSCRMSandboxService
REM net stop MSCRMUnzipService
net stop MSCRMMonitoringService

SET CURRENTDIR=%CD%

PowerShell.exe -NoProfile -file "%CURRENTDIR%\stopCRMAppPool.ps1" -path "%CURRENTDIR%"