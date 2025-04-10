// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
// using Microsoft.Extensions.Logging;
// using static McpTools.ToolsInformation;

// namespace McpTools;
// public class SnippetsTool(ILogger<SnippetsTool> logger)
// {
//     private const string BlobPath = "snippets/{mcptoolargs." + SnippetNamePropertyName + "}.json";

//     [Function(nameof(GetSnippet))]
//     public object GetSnippet(
//         [McpToolTrigger(GetSnippetToolName, GetSnippetToolDescription)]
//             ToolInvocationContext context,
//         [BlobInput(BlobPath)] string snippetContent
//     )
//     {
//         return snippetContent;
//     }

//     [Function(nameof(SaveSnippet))]
//     [BlobOutput(BlobPath)]
//     public string SaveSnippet(
//         [McpToolTrigger(SaveSnippetToolName, SaveSnippetToolDescription)]
//             ToolInvocationContext context,
//         [McpToolProperty(SnippetNamePropertyName, PropertyType, SnippetNamePropertyDescription)]
//             string name,
//         [McpToolProperty(SnippetPropertyName, PropertyType, SnippetPropertyDescription)]
//             string snippet
//     )
//     {
//         return snippet;
//     }
// }