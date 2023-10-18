using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BOT_V2.Binance.Common
{
    //
    // Summary:
    //     Binance base class for REST sections of the API.
    public abstract class BinanceService
    {
        private static readonly string UserAgent = "binance-connector-dotnet/" + VersionInfo.GetVersion;

        private string apiKey;

        private IBinanceSignatureService signatureService;

        private string baseUrl;

        private HttpClient httpClient;

        public BinanceService(HttpClient httpClient, string baseUrl, string apiKey, string apiSecret)
        {
            this.httpClient = httpClient;
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
            signatureService = new BinanceHmac(apiSecret);
        }

        public BinanceService(HttpClient httpClient, string baseUrl, string apiKey, IBinanceSignatureService signatureService)
        {
            this.httpClient = httpClient;
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
            this.signatureService = signatureService;
        }

        protected async Task<T> SendPublicAsync<T>(string requestUri, HttpMethod httpMethod, Dictionary<string, object> query = null, object content = null)
        {
            if (query != null)
            {
                StringBuilder stringBuilder = BuildQueryString(query, new StringBuilder());
                if (stringBuilder.Length > 0)
                {
                    requestUri = requestUri + "?" + stringBuilder.ToString();
                }
            }

            return await SendAsync<T>(requestUri, httpMethod, content);
        }

        protected async Task<T> SendSignedAsync<T>(string requestUri, HttpMethod httpMethod, Dictionary<string, object> query = null, object content = null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (query != null)
            {
                stringBuilder = BuildQueryString(query, stringBuilder);
            }

            string str = signatureService.Sign(stringBuilder.ToString());
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append("&");
            }

            stringBuilder.Append("signature=").Append(HttpUtility.UrlEncode(str));
            requestUri = requestUri + "?" + stringBuilder.ToString();
            return await SendAsync<T>(requestUri, httpMethod, content);
        }

        private StringBuilder BuildQueryString(Dictionary<string, object> queryParameters, StringBuilder builder)
        {
            foreach (KeyValuePair<string, object> queryParameter in queryParameters)
            {
                string text = Convert.ToString(queryParameter.Value, CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }

                    builder.Append(queryParameter.Key).Append("=").Append(HttpUtility.UrlEncode(text));
                }
            }

            return builder;
        }

        private async Task<T> SendAsync<T>(string requestUri, HttpMethod httpMethod, object content = null)
        {
            using HttpRequestMessage request = new HttpRequestMessage(httpMethod, baseUrl + requestUri);
            request.Headers.Add("User-Agent", UserAgent);
            if (content != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            if (apiKey != null)
            {
                request.Headers.Add("X-MBX-APIKEY", apiKey);
            }

            HttpResponseMessage response = await httpClient.SendAsync(request);
            using HttpContent responseContent = response.Content;
            if (response.IsSuccessStatusCode)
            {
                //using (HttpContent responseContent = response.Content)
                {
                    string text = await responseContent.ReadAsStringAsync();
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)text;
                    }

                    try
                    {
                        return JsonConvert.DeserializeObject<T>(text);
                    }
                    catch (JsonReaderException innerException)
                    {
                        throw new BinanceClientException("Failed to map server response from '$" + requestUri + "' to given type", -1, innerException)
                        {
                            StatusCode = (int)response.StatusCode,
                            Headers = response.Headers.ToDictionary<KeyValuePair<string, IEnumerable<string>>, string, IEnumerable<string>>((KeyValuePair<string, IEnumerable<string>> a) => a.Key, (KeyValuePair<string, IEnumerable<string>> a) => a.Value)
                        };
                    }
                }
            }

            //using HttpContent responseContent = response.Content;
            string text2 = await responseContent.ReadAsStringAsync();
            int statusCode = (int)response.StatusCode;
            BinanceHttpException ex;
            if (400 <= statusCode && statusCode < 500)
            {
                if (string.IsNullOrWhiteSpace(text2))
                {
                    ex = new BinanceClientException("Unsuccessful response with no content", -1);
                }
                else
                {
                    try
                    {
                        ex = JsonConvert.DeserializeObject<BinanceClientException>(text2);
                    }
                    catch (JsonReaderException innerException2)
                    {
                        ex = new BinanceClientException(text2, -1, innerException2);
                    }
                }
            }
            else
            {
                ex = new BinanceServerException(text2);
            }

            ex.StatusCode = statusCode;
            ex.Headers = response.Headers.ToDictionary<KeyValuePair<string, IEnumerable<string>>, string, IEnumerable<string>>((KeyValuePair<string, IEnumerable<string>> a) => a.Key, (KeyValuePair<string, IEnumerable<string>> a) => a.Value);
            throw ex;
        }
    }
}