using Newtonsoft.Json.Linq;

namespace DesmodusWATemplate.DTOs
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; } = default(T);
    }
}
