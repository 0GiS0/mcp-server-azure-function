
# Azure Functions loves MCP Servers

Para crear servidores MCP apoyandonos en Azure Functions podemos utilizar esta librería:

```bash
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.Mcp --version 1.0.0-preview.2
``` 

Aquí tienes toda la información sobre este paquete de Nuget: https://www.nuget.org/packages/Microsoft.Azure.Functions.Worker.Extensions.Mcp

Para probarlo puedes usar MCP inspector:

```bash
npx @modelcontextprotocol/inspector
```

En la interfaz hay que seleccionar `Transport type`como `SSE`y la URL a añadir es: `http://0.0.0.0:7071/runtime/webhooks/mcp/sse``

