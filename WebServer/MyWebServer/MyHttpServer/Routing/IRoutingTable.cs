namespace MyWebServer.MyHttpServer.Routing
{
    using MyWebServer.MyHttpServer.Http;

    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);

        IRoutingTable MapGet(string path, HttpResponse response);
    }
}
