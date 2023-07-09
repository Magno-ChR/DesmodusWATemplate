using DesmodusWATemplate.DTOs;

namespace DesmodusWATemplate.Helpers
{
    public interface IResponse
    {
        Task<ResponseDto<T>> GetRequest<T>(string apiUrl);
    }
}
