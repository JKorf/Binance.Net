var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.UseWebSockets();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("Starting");
app.Run();
