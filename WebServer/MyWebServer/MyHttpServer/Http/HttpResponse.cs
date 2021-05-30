namespace MyWebServer.MyHttpServer.Http
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; init; }

        public HttpHeaderCollection Header { get; } = new HttpHeaderCollection();

        public string Content { get; init; }
    }
}
