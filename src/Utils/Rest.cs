using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Magic
{
    public interface IRest
    {
        Task<TResponse> Get<TResponse>(string url, IDictionary<string, string> query = null);
        Task<TResponse> Post<TBody, TResponse>(string url, TBody body);
    }

    public class Rest : IRest
    {
        public class MagicAPIResponse<TData>
        {
            [JsonProperty("data")]
            public TData Data { get; set; }

            [JsonProperty("error_code")]
            public string ErrorCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }
        }

        private readonly HttpClient Client;
        private readonly string SecretApiKey;

        public Rest(string secretApiKey, HttpClient httpClient = null)
        {
            SecretApiKey = secretApiKey;
            var client = httpClient ?? new HttpClient();
            client.DefaultRequestHeaders.Add("X-Magic-Secret-Key", secretApiKey);
            Client = client;
        }

        public async Task<TResponse> Post<TBody, TResponse>(string url, TBody body)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
                var result = await Client.SendAsync(requestMessage);
                var stringContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<MagicAPIResponse<TResponse>>(stringContent);
                if (response.Status != "ok")
                {
                    throw new MagicServiceException();
                }

                return response.Data;
            }
            catch (System.Exception ex)
            {
                throw new MagicServiceException(ex.Message);
            }
        }

        public async Task<TResponse> Get<TResponse>(string url, IDictionary<string, string> query = null)
        {
            try
            {
                var builder = new UriBuilder(url);
                if (query?.Count > 0)
                {
                    var qs = HttpUtility.ParseQueryString(builder.Query);
                    foreach (var param in query)
                    {
                        qs.Add(param.Key, param.Value);
                    }
                    builder.Query = qs.ToString();
                }
                var constructedUrl = builder.ToString();

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, constructedUrl);
                var result = await Client.SendAsync(requestMessage);
                var stringContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<MagicAPIResponse<TResponse>>(stringContent);
                if (response.Status != "ok")
                {
                    throw new MagicServiceException();
                }

                return response.Data;
            }
            catch (System.Exception ex)
            {
                throw new MagicServiceException(ex.Message);
            }
        }
    }
}