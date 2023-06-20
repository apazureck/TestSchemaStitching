// See https://aka.ms/new-console-template for more information

using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>(d =>
    {
        d.Name("Query");
    })
    .AddDiagnosticEventListener<ConsoleQueryLogger>()
    .AddObjectType<Group>(d => d.Field(g => g .UserIds).Type<ListType<IdType>>());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();

public record Group(ICollection<string> UserIds, string GroupName)
{
    
}

public class Query
{
    public Group GetGroup() => new Group(new List<string> {"1", "2"}, "Group 1");
}