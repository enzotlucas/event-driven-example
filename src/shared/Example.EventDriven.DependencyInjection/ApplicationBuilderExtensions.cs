using Example.EventDriven.DependencyInjection.Swagger;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiDependencyInjection(this IApplicationBuilder app)
        {
            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwaggerConfiguration();

            return app;
        }

        public static IApplicationBuilder UseWorkerDependencyInjection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
