using Infrastructure;
using WebApi;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);

//builder.WebHost.ConfigureKestrel(so => { so.ListenAnyIP(8080); });

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    //192.168.125.70
    serverOptions.ListenAnyIP(8080);
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
