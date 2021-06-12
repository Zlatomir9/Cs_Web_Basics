namespace MyWebServer.MyHttpServer.Responses
{
    using MyWebServer.MyHttpServer.Http;

    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html) 
            : base(html, HttpContentTypes.Html)
        {
        }
    }
}
