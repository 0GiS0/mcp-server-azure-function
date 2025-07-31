# Azure Functions ⚡️❤️ MCP Servers

> **Idiomas / Languages:** [🇪🇸 Español](README.md) | [🇺🇸 English](README_en.md)

Hello developer 👋🏻! This repo is part of a video from my YouTube channel that shows how we can create MCP (Model Context Protocol) servers using Azure Functions and use them with GitHub Copilot Chat agent mode.

[![Watch the video on YouTube](images/Portada%20Video%20de%20YouTube%20MCP%20servers%20con%20Azure%20Functions.png)](https://github.com/0GiS0/mcp-server-azure-function)

## NuGet package for creating MCP servers 📦

To create MCP servers using Azure Functions we can use this library:

```bash
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.Mcp --version 1.0.0-preview.2
``` 

Here you have all the information about this NuGet package: https://www.nuget.org/packages/Microsoft.Azure.Functions.Worker.Extensions.Mcp

To test this sample code, you need to run the project locally:

```bash
func start
```

And you can use MCP inspector:

```bash
npx @modelcontextprotocol/inspector http://localhost:7071/runtime/webhooks/mcp/sse
```

You can also use GitHub Copilot Chat to interact with this MCP server. For this you just need to create the `.vscode/mcp.json` file or you can include this section inside the `.vscode/settings.json` file:


```javascript
{
    "inputs": [
        {
            "type": "promptString",
            "id": "mcp-azure-function-key",
            "description": "Azure Function Key to access the MCP server on Azure",
            "password": true
        },
        {
            "type": "promptString",
            "id": "mcp-azure-function-name",
            "description": "Azure Function name to access the MCP server on Azure"
        }
    ],
    "servers": {
        "local-mcp-azure-function": {
            "type": "sse",
            "url": "http://localhost:7071/runtime/webhooks/mcp/sse",
        },
        // "remote-mcp-azure-function": {
        //     "type": "sse",
        //     "url": "https://${input:mcp-azure-function-name}.azurewebsites.net/runtime/webhooks/mcp/sse",
        //     "headers": {
        //         "x-functions-key": "${input:mcp-azure-function-key}"
        //     }
        // }
    }
}
```

You can also add the MCP through the command line:

```bash
code --add-mcp '{"name": "local-mcp", "type":  "sse", "url": "http://localhost:7071/runtime/webhooks/mcp/sse"}'
```
## GitHub Copilot Chat configuration for using the MCP server 🛠️

This configuration consists of two main parts:

- `inputs`: which will allow us to avoid hardcoding certain sensitive content
- `servers`: which will be all those servers, local and remote, that we will be able to enable for GitHub Copilot Chat.

In this example I have the configuration for two servers, the Azure Function that I'm running in development environment and the Azure Function that I already have deployed in Microsoft Azure. As you can see in this second configuration, I use the inputs defined in the first section to use the name of the created Azure Function and the key associated with it to be able to access it.

## Creating an Azure Function in Azure ⚡️

To create an Azure Function in Azure, you can use the Terraform code hosted in the `infra` directory, but first you need to create a `terraform.tfvars` file with the following information:

```hcl
# Azure subscription ID
subscription_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
# API Key for YouTube, which you could create here: https://console.cloud.google.com/apis/credentials
youtube_api_key = "XXXXXXXXXXXX"
```

Once you have it, you need to log in with Azure CLI:

```bash
az login
```

And then you can run the following commands to create the Azure Function:

```bash
terraform init
terraform apply
```
This will create an Azure Function in Azure and configure it to use the code from this repository. Remember that you must have Azure credentials configured on your local machine.

> [!NOTE]
> Once you have the infrastructure, you must deploy the code of your Azure Function. You can do it easily using the Visual Studio Code plugin.

Once you have it, when you try to start the MCP server, it will ask you for the name of the function you want to use in Azure and the master key, which you can find in the Azure portal.


See you later 👋🏻!