using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Example.EventDriven.DependencyInjection.Swagger
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerExtensions
    {
        internal static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                options.CustomSchemaIds(SchemaIdStrategy);

                options.IncludeCommentsToApiDocumentation();
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }

        private static string SchemaIdStrategy(Type currentClass)
        {
            return currentClass.Name.Replace("ViewModel", string.Empty).Replace("Model", string.Empty);
        }

        private static void IncludeCommentsToApiDocumentation(this SwaggerGenOptions options)
        {
            try
            {
                options.TryIncludeCommentsToApiDocumentation();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TryIncludeCommentsToApiDocumentation(this SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";

            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            options.IncludeXmlComments(xmlPath);
        }


        internal static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {

                    foreach (var groupName in provider.ApiVersionDescriptions.Select(description => description.GroupName))
                        options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
                });

            return app;
        }
    }
}
