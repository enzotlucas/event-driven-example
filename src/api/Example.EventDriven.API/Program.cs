using Example.EventDriven.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDependencyInjection(builder.Configuration);

var app = builder.Build();

app.UseApiDependencyInjection();

app.MapControllers();

app.Run();
