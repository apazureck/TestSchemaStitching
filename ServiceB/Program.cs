// See https://aka.ms/new-console-template for more information

using Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddGraphQLServer()
    .AddDiagnosticEventListener<ConsoleQueryLogger>()
    .AddQueryType<Query>()
    .AddObjectType<User>(d => d.Field(u => u.Id).ID(nameof(User)));
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.Run();

public record User(string Id, string FirstName, string LastName);

public class Query
{
    private ICollection<User> UserList { get; } = new List<User>()
    {
        new User("1", "Max", "Mustermann"),
        new User("2", "Stephanie", "Musterfrau")
    };

    public ICollection<User?> GetUsersById([ID] string[] ids)
    {
        return ids.Select(id => UserList.FirstOrDefault(x => x.Id == id)).ToList();
    }
}