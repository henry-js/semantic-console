# README

```pwsh
$repoRoot = (Get-Item $PSScriptRoot).Parent
Set-Location "$repoRoot/src/Cli"
dotnet user-secrets init
dotnet user-secrets set "OpenAI:ChatModelId" "gpt-3.5-turbo"
dotnet user-secrets set "OpenAI:ApiKey" "<Enter Api Key>"
Set-Location -
```
