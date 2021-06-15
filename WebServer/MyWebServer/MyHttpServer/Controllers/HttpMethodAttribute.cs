namespace MyWebServer.MyHttpServer.Controllers
{
    using System;
    using MyWebServer.MyHttpServer.Http;

    public abstract class HttpMethodAttribute : Attribute
    {
        protected HttpMethodAttribute(HttpMethod htttpMethod)
            => this.HttpMethod = htttpMethod;

        public HttpMethod HttpMethod { get; }
    }
}
