namespace MyWebServer
{
    using MyWebServer.MyHttpServer;
    using System.Threading.Tasks;
    public class StartUp
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer("127.0.0.1", 9090);

            await server.Start();
        }
    }
}
