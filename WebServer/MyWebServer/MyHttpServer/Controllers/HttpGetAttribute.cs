namespace MyWebServer.MyHttpServer.Controllers
{
    using MyWebServer.MyHttpServer.Http;

    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() 
            : base(HttpMethod.Get)
        {
        }
    }
}
