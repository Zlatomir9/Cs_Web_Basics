namespace MyWebServer.MyHttpServer.Results
{
    using MyWebServer.MyHttpServer.Http;

    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string html) 
            : base(response, html, HttpContentType.Html)
        {
        }
    }
}
