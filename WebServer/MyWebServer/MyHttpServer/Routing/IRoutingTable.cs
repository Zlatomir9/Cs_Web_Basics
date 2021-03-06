namespace MyWebServer.MyHttpServer.Routing
{
    using MyWebServer.MyHttpServer.Common;
    using MyWebServer.MyHttpServer.Http;
    using System;

    public interface IRoutingTable
    {
        IRoutingTable MapStaticFiles(string folder = Settings.StaticFilеsRootFolder);

        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);

        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction);

        IRoutingTable MapGet(string path, HttpResponse response);

        IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction);

        IRoutingTable MapPost(string path, HttpResponse response);

        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction);
    }
}
