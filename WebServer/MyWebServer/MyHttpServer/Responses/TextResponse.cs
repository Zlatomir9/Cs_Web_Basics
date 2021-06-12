namespace MyWebServer.MyHttpServer.Responses
{
    using MyWebServer.MyHttpServer.Http;

    public class TextResponse : ContentResponse
    {
        public TextResponse(string text) 
            : base(text, HttpContentTypes.PlainText)
        {
        }
    }
}
