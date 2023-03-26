using AmazonDynamoDB.Api.Middlewares;

namespace AmazonDynamoDB.Api.Extensions;

public static class ApplicationBuilderRegistration
{
    /// <summary>
    /// Adiciona as configurações de CORS ao pipeline da aplicação.
    /// </summary>
    /// <param name="app">application builder app original do pipeline da aplicação</param>
    public static void UseCorsConfigure(this IApplicationBuilder app)
    {
        //string[] origins =
        //{
        //    "http://localhost:4200"
        //};

        //app.UseCors(x => x.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader());
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }

    /// <summary>
    /// Adiciona as configurações do swagger ao pipeline da aplicação.
    /// </summary>
    /// <param name="app">application builder app original do pipeline da aplicação</param>
    public static void UseSwaggerConfigure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amazon DynamoDB Web API");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            //c.InjectStylesheet("/swagger-ui/swagger-dark.css");
        });
    }

    /// <summary>
    /// Adiciona as configurações do retorno padrão para errors lançados na api ao pipeline da aplicação.
    /// </summary>
    /// <param name="app">application builder app original do pipeline da aplicação</param>
    public static void UseCustomApiErrorsExceptionConfigure(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomApiErrorExceptionMiddleware>();
    }
}
