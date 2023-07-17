using Newtonsoft.Json.Linq;

namespace DesmodusWATemplate.DTOs
{
    public class ResponseDto<T>
    {
        public ResponseDto() { }

        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; } = default(T);
    }

    public static class ResponseErrorDto {
        public static ResponseDto Error401(this ResponseDto valor)
        {

            valor.StatusCode = 401;
            valor.IsSuccess = false;
            valor.Message = "No estás autorizado para acceder a este recurso.";
            valor.Data = default(T);

            return valor;

        }
    }
    
}
