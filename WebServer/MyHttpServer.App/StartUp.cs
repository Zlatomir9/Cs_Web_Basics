namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.App.Controllers;
    using MyWebServer.MyHttpServer;
    using MyWebServer.MyHttpServer.Controllers;
    
    public class StartUp
    {
        public static async Task Main()
            => await new HttpServer(routes => routes
                .MapStaticFiles()
                .MapControllers()
                .MapGet<HomeController>("/ToCats", c => c.LocalRedirect()))
            .Start();
    }
}