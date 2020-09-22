namespace SIS.WebServer.Result
{
    using System.Text;

    using HTTP.Enums;
    using HTTP.Responses;
    using HTTP.Headers;

    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode responseStatusCode)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader("Content-Type", "text/html; charset=utf-8"));

            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
