using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using static McpTools.ToolsInformation;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.EnableMcpToolMetadata();

// Demonstrate how you can define tool properties without requiring
// input bindings:
builder
    .ConfigureMcpTool(GetSnippetToolName)
    .WithProperty(SnippetNamePropertyName, PropertyType, SnippetNamePropertyDescription);

builder.Build().Run();