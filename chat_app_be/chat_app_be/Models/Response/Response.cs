using System.Net;

namespace chat_app_be.Models.Response
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public Response(int statusCodes, string message)
        {
            StatusCode = statusCodes;
            Message = message;
        }

        public Response(int statusCodes, object data)
        {
            StatusCode = statusCodes;
            Message = "Success";
            Data = data;
        }

        public Response(int statusCodes)
        {
            StatusCode = statusCodes;
            Message = "Success";
        }
    }
}
