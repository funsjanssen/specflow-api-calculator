var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("{x:int}/add/{y:int}", (int x, int y, HttpContext context) => x + y);

await app.RunAsync();

#pragma warning disable S1118
public partial class Program {}

#pragma warning enable S1118