using DesmodusWATemplate.DTOs;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DesmodusWATemplate.Helpers
{
    public class Response : IResponse
    {
        private readonly HttpClient http;
        private readonly IConfiguration configuration;
        private readonly NavigationManager navigationManager;
        private readonly ILocalStorageService localStorage;

        public Response(ILocalStorageService localStorage, HttpClient http, IConfiguration configuration, NavigationManager navigationManager)
        {
            this.localStorage = localStorage;
            this.http = http;
            this.configuration = configuration;
            this.navigationManager = navigationManager;
        }
        private async Task<bool> AddToken()
        {
            string? token = await localStorage.GetItemAsStringAsync("token");
            if (token == null)
                token = string.Empty;
            //if (!string.IsNullOrEmpty(token))
            //{

                http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token!.Replace("\"", ""));
                return true;
            //}
            //else
            //{
            //    navigationManager.NavigateTo("Login");
            //    return false;
            //}
                
            
        }
        private async Task<Respuesta<T>> toResponse<T>(HttpResponseMessage response)
        {
            int statusCode = (int)response.StatusCode;

            // Leer el mensaje de respuesta
            string responseMessage = response.ReasonPhrase.ToString();

            // Obtener el contenido de respuesta
            string responseBody = await response.Content.ReadAsStringAsync();

            if (String.IsNullOrEmpty(responseBody))
            {
                return new Respuesta<T>{ StatusCode =  statusCode, Message = responseMessage };
            }
            // Deserializar el responseBody a un objeto de tipo T
            Respuesta<T> RespuestaDto = JsonConvert.DeserializeObject<Respuesta<T>>(responseBody);

            return RespuestaDto;
        }
        public async Task<Respuesta<T>> GetRequest<T>(string apiUrl)
        {
            Respuesta<T> responseDto;
            // Asignar Token
            if (await AddToken())
            {
                // Realizar la solicitud GET
                HttpResponseMessage response = await http.GetAsync(apiUrl);

                // Utilizar el método toResponse para obtener el objeto ResponseDto<T>
                responseDto = await toResponse<T>(response);

                return responseDto;
            }
            else
                return responseDto =  new Respuesta<T>{ Message = "No autorizado", StatusCode = 401 };
            
        }
        public async Task<Respuesta<T>> PostRequest<T,Tvalue>(string apiUrl, Tvalue content)
        {
            Respuesta<T> responseDto;
            // Asignar Token
            if (await AddToken())
            {
                var enviarJSON = System.Text.Json.JsonSerializer.Serialize(content);
                // Realizar la solicitud GET
                var httpContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await http.PostAsync(apiUrl, httpContent);

                // Utilizar el método toResponse para obtener el objeto ResponseDto<T>
                responseDto = await toResponse<T>(response);

                return responseDto;
            }
            else
                return responseDto = new Respuesta<T> { Message = "No autorizado", StatusCode = 401 };

        }
    }
}
