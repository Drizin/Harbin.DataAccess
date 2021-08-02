# To Execute Powershell Scripts from Visual Studio:
# 1) Right-button PS1 file - "Open With...""
# 2) Configure:
#      Program: C:\Program Files\PowerShell\7\pwsh.exe
#      Arguments: -noexit -File %1
#      Friendly Name: Execute PowerShell Script
# If Powershell Core is not installed, download it (e.g. PowerShell-7.1.3-win-x64.msi) here: https://aka.ms/powershell-release?tag=stable 

$dir = Split-Path $MyInvocation.MyCommand.Path 
Set-Location $dir

$ErrorActionPreference = "Stop"



$stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
Write-host "Starting codegencs dbschema-extractor ..." -for yellow

# TO INSTALL: dotnet tool install -g dotnet-codegencs
& codegencs extract-dbschema mssql "Data Source=(local)\SQLEXPRESS; Initial Catalog=AdventureWorks2019; Integrated Security=True;" ..\CoreDbSchema.json

$stopwatch.Stop()
Write-Host "Finished in $($stopwatch.Elapsed.TotalMilliSeconds) milliseconds"

# Since I configured "-noexit" parameter in Visual Studio I don't need this
#if ($host.Name -notmatch 'ISE') { Write-Host -NoNewLine "(Just press Enter to exit)" -for cyan; read-host; }  
