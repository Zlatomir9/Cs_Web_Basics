namespace MyWebServer.MyHttpServer.Responses
{
    using MyWebServer.MyHttpServer.Common;
    using MyWebServer.MyHttpServer.Http;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType) 
            : base(HttpStatusCode.OK)
            => this.PrepareContent(content, contentType);
    }
}
