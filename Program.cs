using FlowAPI.Endpoints;
using FlowAPI.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProcessEngine>();

var app = builder.Build();
app.RegisterDefinitionEndpoints();
app.RegisterInstanceEndpoints();

app.Run();
