using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class TransseeApiResponse
    {
        public string? Stop { get; set; }
        public JsonElement Routes { get; set; }
        public JsonElement Vehicles { get; set; }
    }

    public class TransseeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://42cummer-transseeapi.hf.space/seek";

        public TransseeApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<TransseeApiResponse?> GetStopInfoAsync(string stopId)
        {
            try
            {
                // Create JSON payload
                var payload = new { stop = stopId };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make POST request
                var response = await _httpClient.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();

                // Read and parse response
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TransseeApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling API: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

