
@ECHO OFF
SET CURRENTDIR=%CD%

SET /P CONTINUE=The following installation assumes you are working on virgin Core 6 RC base. Do you want to continue (Y/N)? 
if /i {%CONTINUE%}=={y} (goto :yes) 
if /i {%CONTINUE%}=={yes} (goto :yes) 
goto :no 

:yes 

ECHO ### Installing CRM Client Event ###

REM copy template files
set COPYTEMPLATESCMD=xcopy /s /y "%CURRENTDIR%\1 CRM Client Event Wizard\Templates\*" "c:\Program Files (x86)\K2 blackpearl\Bin\DesignTemplates\CSharp" 
%COPYTEMPLATESCMD%

REM gac wizard files
set GACWIZARDCMD="%CURRENTDIR%\1 CRM Client Event Wizard\gacutil.exe" /i "%CURRENTDIR%\1 CRM Client Event Wizard\WizardCRMClient.dll"
%GACWIZARDCMD%

set GACDESIGNCMD="%CURRENTDIR%\1 CRM Client Event Wizard\gacutil.exe" /i "%CURRENTDIR%\1 CRM Client Event Wizard\DesignCRMClient.dll"
%GACDESIGNCMD%

REM update C:\ProgramData\SourceCode\configurationmanager.config
set UPDATECONFIGMGRCMD=PowerShell.exe -NoProfile -file "%CURRENTDIR%\updateConfigurationManager.ps1" -path "%CURRENTDIR%"
%UPDATECONFIGMGRCMD%


ECHO ### Copying K2 Dynamics CRM Functions Broker ###

REM copy files to servicebroker
set COPYBROKERFILESCMD=xcopy /y "%CURRENTDIR%\2 K2 CRM Functions Broker\*" "C:\Program Files (x86)\K2 blackpearl\ServiceBroker"
%COPYBROKERFILESCMD%

set COPYBROKERCONFIGCMD=xcopy "%CURRENTDIR%\2 K2 CRM Functions Broker\K2.Demo.CRM.Functions.ServiceBroker.xml" "C:\Program Files (x86)\K2 blackpearl\Host Server\Bin"
%COPYBROKERCONFIGCMD%


REM copy sdk files to servicebroker
set COPYSDKFILESCMD=xcopy /y "%CURRENTDIR%\3 CRM 2013 SDK\*" "C:\Program Files (x86)\K2 blackpearl\ServiceBroker"
%COPYBROKERFILESCMD%


ECHO ### Updating K2Services REST web.config to Ntlm ###

REM Update K2Services REST security to Ntlm
set UPDATERESTWEBCONFIGCMD=PowerShell.exe -NoProfile -file "%CURRENTDIR%\updateK2ServiceRESTWebConfig.ps1" -path "%CURRENTDIR%"
%UPDATERESTWEBCONFIGCMD%


REM restart K2 blackpearl

ECHO ### The K2 service must be restarted to pick up the new assemblies
PAUSE

NET STOP "K2 blackpearl Server"
PAUSE

NET START "K2 blackpearl Server"


REM deploy CRM solutions - manual

REM update crm sitemap - manual

REM create service type

REM deploy package with instance and smartobjects

ECHO ###
ECHO The following steps now must be completed manually 
ECHO 1) In K2 Workspace you need to give the CRM Service account impersonate permissions in Server Rights
ECHO 2) In Package and Deploy deploy the K2 for Dynamics CRM Demo package   
ECHO 3) In SmartObject Tester register a Service Type of K2 Dynamics CRM Functions K2.Demo.CRM.Functions.ServiceBroker.dll 
ECHO 4) In Dynamics CRM 2013 on-premises deploy the K2 for Dynamics CRM 2013 - Process solution 
ECHO 5) In Dynamics CRM 2013 on-premises deploy the K2 for Dynamics CRM 2013 - Forms solution
ECHO 6) In Dynamics CRM 2013 on-premises deploy the K2 for Dynamics CRM 2013 - Sitemap Demo solution 
ECHO 7) In Dynamics CRM 2013 on-premises import the K2 Settings.txt file into the K2 Settings entity
ECHO ###


pause
exit /b 1

:no
ECHO cancelled
exit /b 1