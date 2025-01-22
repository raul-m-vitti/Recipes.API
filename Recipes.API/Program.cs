using Microsoft.OpenApi.Models;
using Recipes.API;
using Recipes.API.Configurations;
using Recipes.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

//Configurando em arquivos separados
builder.Services.ConfigureSwagger();
builder.Services.ConfigureDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware para gerenciar erros
app.UseMiddleware<ExceptionMiddleware>();

//Mapeamento das rotas em arquivo separado
app.MapApi();

app.UseHttpsRedirection();

app.Run();