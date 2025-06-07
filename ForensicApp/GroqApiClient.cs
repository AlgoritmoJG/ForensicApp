using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForensicApp
{
    internal class GroqApiClient
    {



            private static readonly HttpClient httpClient = new HttpClient();
            private const string GroqApiKey = ""; 
            private const string GroqApiUrl = ""; 

            public static async Task<string> ObtenerRespuestaGroq(string promptContent)
            {
                var requestPayload = new GroqRequest
                {
                    Messages = new GroqMessage[]
                    {
                    new GroqMessage { Role = "user", Content = promptContent }
                    },
                    Model = "llama3-8b-8192", 
                };

                var jsonPayload = JsonSerializer.Serialize(requestPayload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GroqApiKey);

                HttpResponseMessage response = await httpClient.PostAsync(GroqApiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    GroqResponse groqResponse = JsonSerializer.Deserialize<GroqResponse>(jsonResponse);

                    if (groqResponse != null && groqResponse.Choices != null && groqResponse.Choices.Length > 0)
                    {
                        return groqResponse.Choices[0].Message.Content;
                    }
                    return "No se recibió una respuesta válida.";
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    // Considera lanzar una excepción más específica o manejar el error de forma más robusta
                    throw new Exception($"Error al llamar a la API de Groq: {response.StatusCode} - {errorContent}");
                }
            }
        }
    }
