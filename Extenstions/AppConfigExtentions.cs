namespace Authentication_Authorization_api.Extenstions
{
    public static class AppConfigExtentions
    {

        public static WebApplication ConfigCORS(this WebApplication app,IConfiguration Config)
        {
            app.UseCors();
            return app;
        }
    }
}
