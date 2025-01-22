using Microsoft.OpenApi.Models;

namespace Recipes.API.Configurations;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Recipes API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Raul Vitti",
                    Email = "raulmv33@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/")
                },
                Description = "Fiz a API utilizando ASP.NET Core 8 com SQL Server e Dapper como ORM. "
                + "Tentei implementar diferentes abordagens em alguns processos como prova de conceito. "
                + "Agradeço a oportunidade e espero que gostem!"
            });

            var xmlFile = "Recipes.API.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);
        });
    }
}