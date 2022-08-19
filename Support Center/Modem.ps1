$ipv4 = (Get-WmiObject -Class Win32_NetworkAdapterConfiguration | where {$_.DHCPEnabled -ne $null -and $_.DefaultIPGateway -ne $null}).IPAddress | Select-Object -First 1
$ipv4 = $ipv4 -replace ".{1}$","1"
$os = (Get-WmiObject -class Win32_OperatingSystem).Caption
$ipv4

if ($os -match "Windows 10"){
[system.Diagnostics.Process]::Start("msedge",('https://'+$ipv4))
"Win10"}
if ($os -match "Windows 7"){
Start-Process -FilePath 'iexplore.exe' -ArgumentList ('https://'+$ipv4)
"Win7"}

