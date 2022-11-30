using WebApiCasino;

var builder = WebApplication.CreateBuilder(args);
var startup = new StartUp(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore().AddApiExplorer();
var app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();