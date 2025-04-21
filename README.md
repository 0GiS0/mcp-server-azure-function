
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

También puedes usar GitHub Copilot Chat para interactuar con este MCP server. Para ello solo tienes que crear el archivo `.vscode/mcp.json` o puedes incluir esta sección dentro del archivo `.vscode/settings.json`:


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

Este configuración se compone de dos partes principales:

- `inputs`: que nos van a permitir no tener que harcodear cierto contenido sensible
- `servers`: que van a ser todos aquellos servidores, locales y remotos, que vamos a poder habilitar para GitHub Copilot Chat.

En este ejemplo tengo la configuración para dos servidores, la Azure Function que estoy ejecutando en entorno de desarrollo y la Azure Function que ya tengo desplegada en Microsoft Azure. Como puedes ver en esta segunda configuración, utilizo los inputs definidos en el primer apartado para utilizar el nombre de la Azure Function creada y la key asociada a la misma para poder acceder.

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

> [!NOTE]
> Una vez tengas la infrastructura, debes desplegar el código de tu Azure Function. Puedes hacerlo de forma sencilla usando el plugin de Visual Studio Code.

Una vez lo tengas, cuando intentes iniciar el servidor MCP, te pedirá el nombre de la función que quieres usar en Azure y la master key, que podrás encontrarlas en el portal de Azure.


¡Nos vemos 👋🏻!