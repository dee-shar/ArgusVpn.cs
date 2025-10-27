using System;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace ArgusVpnApi
{
    public class ArgusVpn
    {
        private string accessToken;
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "https://ar-main.com";
        public ArgusVpn()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Dart/2.19 (dart:io)");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Login(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password
            });
            var response = await httpClient.PostAsync($"{apiUrl}/auth/login", data);
            var content = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(content);
            if (document.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Object)
            {
                if (dataElement.TryGetProperty("access_token", out var tokenElement) && tokenElement.ValueKind == JsonValueKind.String)
                {
                    string accessToken = tokenElement.GetString();
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                }
            }
            return content;
        }

        public async Task<string> Register(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password,
                repeat_password = password,
                country = "",
                referral_code = "",
            });
            var response = await httpClient.PostAsync($"{apiUrl}/auth/registration", data);
            var content = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(content);
            if (document.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Object)
            {
                if (dataElement.TryGetProperty("access_token", out var tokenElement) && tokenElement.ValueKind == JsonValueKind.String)
                {
                    string accessToken = tokenElement.GetString();
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                }
            }
            return content;
        }

        public async Task<string> GetServers()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/servers?device=mobile&os=android&");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
