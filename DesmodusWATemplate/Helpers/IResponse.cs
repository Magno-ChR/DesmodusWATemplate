using DesmodusWATemplate.DTOs;

namespace DesmodusWATemplate.Helpers
{
    public interface IResponse
    {
        Task<Respuesta<T>> GetRequest<T>(string apiUrl);
        Task<Respuesta<T>> PostRequest<T, Tvalue>(string apiUrl, Tvalue content);
    }
}
