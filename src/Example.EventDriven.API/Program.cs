using Example.EventDriven.API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjection();

var app = builder.Build();

app.UseDependencyInjection();

app.MapControllers();

app.Run();
