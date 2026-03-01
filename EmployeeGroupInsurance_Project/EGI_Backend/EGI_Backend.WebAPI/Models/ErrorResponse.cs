using System.Text.Json;

namespace EGI_Backend.WebAPI.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public string? TraceId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
