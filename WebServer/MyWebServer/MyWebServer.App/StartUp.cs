namespace MyWebServer
{
    using System.Threading.Tasks;
    using MyWebServer.MyHttpServer;
    using MyWebServer.MyHttpServer.Responses;
    
    public class StartUp
    {
        static async Task Main(string[] args)
            => await new HttpServer(routes => routes
                .MapGet("/", new TextResponse("Hello from the server!"))
                .MapGet("/Dogs", new HtmlResponse("<h3>Hello from the dogs!</h3>")))
            .Start();
    }
}
