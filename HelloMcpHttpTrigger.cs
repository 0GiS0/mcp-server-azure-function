// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
// using Microsoft.Extensions.Logging;
// using static McpTools.ToolsInformation;

// namespace McpTools;

// public class HelloTool(ILogger<SnippetsTool> logger)
// {
//     [Function(nameof(SayHello))]
//     public string SayHello(
//         [McpToolTrigger(HelloToolName, HelloToolDescription)] ToolInvocationContext context
//     )
//     {
//         logger.LogInformation("Saying hello");
//         return "Hello I am MCP Tool!";
//     }
// }