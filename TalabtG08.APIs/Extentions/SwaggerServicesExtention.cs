namespace TalabtG08.APIs.Extentions
{
    public static class SwaggerServicesExtention
    {
        public static IServiceCollection AddSwaggerServices (this IServiceCollection Services)
        {

            Services.AddEndpointsApiExplorer();

            Services.AddSwaggerGen();

            return Services;    

        }

        public static WebApplication UseSwaggerMiddelWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app; 
        }
    }
}
