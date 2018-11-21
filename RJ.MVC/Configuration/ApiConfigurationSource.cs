using RJ.Configuration;
using System;
using System.Configuration;
using System.Net.Http;

namespace RJ.MVC.Configuration {
    public class ApiConfigurationSource : IConfigurationSource {
        private readonly Uri _baseUri;

        public ApiConfigurationSource(Uri baseUri) {
            _baseUri = baseUri;
        }

        public ConnectionStringSettings GetConnectionString(string key) {
            throw new NotImplementedException();
        }

        public string GetValue(string key) {
            using (var client = GetClient()) {
                var value = client.GetStringAsync("config/" + key)
                    .ConfigureAwait(false).GetAwaiter().GetResult();
                return value;
            }
        }

        private HttpClient GetClient() {
            return new HttpClient {
                BaseAddress = _baseUri
            };
        }
    }
}