using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Extensions;
using APICatalogo.Filter;
using APICatalogo.Logging;
using APICatalogo.Repository;
using APICatalogo.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information

}));

string mySqlConnection = 
    builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContextPool<AppDbContext>(options =>
            options.UseMySql(mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

builder.Services.AddTransient<IMeuServico, MeuServico>();

builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//adiciona o middleware de tratamento de erros
app.ConfigureExceptionHandler();
//adiciona o middleware para redirecionar https
app.UseHttpsRedirection();
//middleware de roteamento
app.UseRouting();
//adiciona o middleware que habilita autorização
app.UseAuthorization();
//middleware que mapeia os endpoints dos controllers
app.MapControllers();

app.Run();
