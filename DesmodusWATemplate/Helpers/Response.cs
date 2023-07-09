﻿using DesmodusWATemplate.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DesmodusWATemplate.Helpers
{
    public class Response : IResponse
    {
        private readonly HttpClient http;
        private readonly IConfiguration configuration;
        private readonly ILocalStorageService localStorage;

        public Response(ILocalStorageService localStorage, HttpClient http, IConfiguration configuration)
        {
            this.localStorage = localStorage;
            this.http = http;
            this.configuration = configuration;
        }
        private async Task AddToken()
        {
            string token = await localStorage.GetItemAsStringAsync("token");
            if (!string.IsNullOrEmpty(token))
            {
                
                http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }
        }
        private async Task<ResponseDto<T>> toResponse<T>(HttpResponseMessage response)
        {
            int statusCode = (int)response.StatusCode;

            // Leer el mensaje de respuesta
            string responseContent = response.ReasonPhrase.ToString();

            // Obtener el contenido de respuesta
            string responseBody = await response.Content.ReadAsStringAsync();

            bool IsSuccess = response.IsSuccessStatusCode;

            // Deserializar el responseBody a un objeto de tipo T
            T data = JsonConvert.DeserializeObject<T>(responseBody);

            // Crear el objeto ResponseDto<T>
            var responseDto = new ResponseDto<T>
            {
                StatusCode = statusCode,
                IsSuccess = IsSuccess,
                Message = responseContent,
                Data = data
            };

            return responseDto;
        }
        public async Task<ResponseDto<T>> GetRequest<T>(string apiUrl)
        {
            // Asignar Token
            await AddToken();

            // Realizar la solicitud GET
            HttpResponseMessage response = await http.GetAsync(apiUrl);

            // Utilizar el método toResponse para obtener el objeto ResponseDto<T>
            ResponseDto<T> responseDto = await toResponse<T>(response);

            return responseDto;
        }

    }
}