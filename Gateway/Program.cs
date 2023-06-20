using Logging;

var builder = WebApplication.CreateBuilder(args);

const string Groups = "groups";
const string Users = "users";

builder.Services
    .AddHttpClient(Groups, c => { c.BaseAddress = new Uri("http://localhost:5235/graphql"); });
builder.Services
    .AddHttpClient(Users, c => c.BaseAddress = new Uri("http://localhost:5236/graphql/"));


builder.Services
    .AddGraphQLServer()
    .AddQueryType(d =>
    {
        d.Name("Query");
        d.Field("version").Resolve("1.0.0");
    })
    .AddRemoteSchema(Groups, ignoreRootTypes: true)
    .AddRemoteSchema(Users, ignoreRootTypes: true)
    .AddDiagnosticEventListener<ConsoleQueryLogger>()
    .AddTypeExtensionsFromFile("./Stitching.graphql");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();