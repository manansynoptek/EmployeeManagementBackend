using System.Net;

namespace Employee.Model.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.OK;
        public string? Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
