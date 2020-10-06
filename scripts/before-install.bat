%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -File "%~dp0\before-install.ps1"
powershell.exe -Command "Invoke-WebRequest -Uri 'https://download.visualstudio.microsoft.com/download/pr/d97cfaf4-b17f-46c7-9a11-7f0d25dfd8b0/f76d4fce8e38b289efb9403aab0a0c9f/dotnet-runtime-3.1.5-win-x64.exe' -OutFile 'C:\Users\Administrator\Downloads\dotnet-install.exe'"
C:\Users\Administrator\Downloads\dotnet-install.exe /install /passive
powershell.exe -Command "Invoke-WebRequest -Uri 'https://download.visualstudio.microsoft.com/download/pr/7c30d3a1-f519-4167-b850-b9c49bf2aa0e/dbfa957a76a41a1e1795f59d400d4ccd/dotnet-hosting-3.1.5-win.exe' -OutFile 'C:\Users\Administrator\Downloads\WindowsHostingBundle.exe'"
C:\Users\Administrator\Downloads\WindowsHostingBundle.exe /install /passive
