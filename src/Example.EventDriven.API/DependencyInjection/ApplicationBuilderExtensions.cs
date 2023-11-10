namespace Example.EventDriven.API.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDependencyInjection(this IApplicationBuilder app)
        {
            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwaggerConfiguration();

            return app;
        }
    }
}
