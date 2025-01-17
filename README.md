# Azure AI Demo

## Setting endpoints and keys:
Open the terminal and execute the following commands (replacing with your own values where appropriate).
### Azure AI Language service
```powershell
dotnet user-secrets set "AILanguageServiceEndpoint" "<your Azure AI Language service endpoint URI>" --project '<the full path to Azure AI Demo.csproj>'
```
```powershell
dotnet user-secrets set "AILanguageServiceKey" "<your Azure AI Language service access key>" --project '<the full path to Azure AI Demo.csproj>'
```
### Azure AI Content Safety service
```powershell
dotnet user-secrets set "AIContentSafetyEndpoint" "<your Azure AI Content Safety service endpoint URI>" --project '<the full path to Azure AI Demo.csproj>'
```
```powershell
dotnet user-secrets set "AIContentSafetyKey" "<your Azure AI Content Safety service access key>" --project '<the full path to Azure AI Demo.csproj>'
```