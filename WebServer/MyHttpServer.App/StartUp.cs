namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.App.Controllers;
    using MyWebServer.MyHttpServer;
    using MyWebServer.MyHttpServer.Controllers;
    
    public class StartUp
    {
        static async Task Main()
            => await new HttpServer(routes => routes
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
                .MapGet<HomeController>("/Softuni", c => c.ToSoftUni())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles()))
            .Start();
    }
}
