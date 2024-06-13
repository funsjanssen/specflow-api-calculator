var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("{x:int}/add/{y:int}", (int x, int y, HttpContext context) => x + y);

app.Run();

public partial class Program {}