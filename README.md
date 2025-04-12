
# Azure Functions ⚡️❤️ MCP Servers

¡Hola developer 👋🏻! Este repo forma parte de un vídeo de mi canal de YouTube que muestra cómo podemos crear MCP (Model Context Protocol) servers apoyádonos en Azure Functions y usarlos con el modo agente de GitHub Copilot Chat.

## Paquete de NuGet para poder crear servidores MCP 📦

Para crear servidores MCP apoyandonos en Azure Functions podemos utilizar esta librería:

```bash
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.Mcp --version 1.0.0-preview.2
``` 

Aquí tienes toda la información sobre este paquete de Nuget: https://www.nuget.org/packages/Microsoft.Azure.Functions.Worker.Extensions.Mcp

Para probar este código de ejemplo, necesitas ejecutar el proyecto en local:

```bash
func start
```

Y puedes usar MCP inspector:

```bash
npx @modelcontextprotocol/inspector http://localhost:7071/runtime/webhooks/mcp/sse
```

## Crear una Azure Function en Azure ⚡️

Para crear una Azure Function en Azure, puedes usar el código en Terraform alojado en el directorio `infra`, pero antes necesitas crear un archivo `terraform.tfvars` con la siguiente información:

```hcl
# ID de la suscripción de Azure
subscription_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
# API Key para YouTube, la cual podrías crear aquí: https://console.cloud.google.com/apis/credentials
youtube_api_key = "XXXXXXXXXXXX"
```

Una vez lo tengas, necesitas iniciar sesión con Azure CLI:

```bash
az login
```

Y después ya puedes ejecutar los siguientes comandos para crear la Azure Function:

```bash
terraform init
terraform apply
```
Esto creará una Azure Function en Azure y la configurará para que use el código de este repositorio. Recuerda que debes tener configuradas las credenciales de Azure en tu máquina local.


¡Nos vemos 👋🏻!