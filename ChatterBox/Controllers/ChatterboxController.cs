using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ChatterBox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatterboxController : ControllerBase
    { 
        private readonly ILogger<ChatterboxController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatterboxController(ILogger<ChatterboxController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost(Name = "generate-image")]
        public async Task<IActionResult> GenerateImage([FromBody] string prompt="Went hiking and loved the scenic views.")
        { 
            var apiKey = "";  // Your OpenAI API key
            if (string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("API key is missing or empty.");
                return StatusCode(500, "Internal server error: API key is missing.");
            }
            var requestUri = new Uri("https://api.openai.com/v1/images/generations");
            var requestBody = new
            {
                model = "dall-e-2",
                prompt = prompt,
                n = 1,
                size = "256x256"
            };

            var requestJson = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = requestContent
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            _logger.LogInformation($"Request URI: {requestUri}");
            _logger.LogInformation($"Request Headers: {requestMessage.Headers}");
            _logger.LogInformation($"Request Body: {requestJson}");
            var client = _httpClientFactory.CreateClient("IgnoreSSL");

            try
            {
                var response = await client.SendAsync(requestMessage);
                var responseData = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error generating image. Status Code: {response.StatusCode}, Response: {responseData}");
                    return StatusCode((int)response.StatusCode, responseData);
                }

                _logger.LogInformation($"Response: {responseData}");
                return Ok(responseData);
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "HTTP request error");
                return StatusCode((int)httpRequestException.StatusCode, httpRequestException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating image");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
