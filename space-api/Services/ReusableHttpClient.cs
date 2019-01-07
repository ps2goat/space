using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceApi.Services
{
    public class ReusableHttpClient : IReusableHttpClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _jsonSettings;

        public ReusableHttpClient()
        {
            _client = new HttpClient();

            _jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            string jsonStringResult = await this._client.GetStringAsync(requestUri);

            return DeserializeJson<T>(jsonStringResult);
        }

        public T DeserializeJson<T>(string jsonStringResult)
        {
            return JsonConvert.DeserializeObject<T>(jsonStringResult, _jsonSettings);
        }
    }
}