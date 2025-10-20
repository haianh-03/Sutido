using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Sutido.Service.Implementations
{
    public class PayOSService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PayOSService(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api-merchant.payos.vn/v2/")
            };
        }

        public async Task<string> CreatePaymentLinkAsync(string orderId, decimal amount, string description)
        {
            var payos = _config.GetSection("PayOS");
            var payload = new
            {
                orderCode = orderId,
                amount = amount,
                description = description,
                cancelUrl = payos["CancelUrl"],
                returnUrl = payos["ReturnUrl"]
            };

            var json = JsonSerializer.Serialize(payload);
            var request = new HttpRequestMessage(HttpMethod.Post, "payment-requests")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("x-client-id", payos["ClientId"]);
            request.Headers.Add("x-api-key", payos["ApiKey"]);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"PayOS error: {content}");

            return content; // JSON response chứa link thanh toán
        }
    }
}
