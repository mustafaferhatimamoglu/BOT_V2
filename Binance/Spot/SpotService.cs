using BOT_V2.Binance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Spot
{
    public abstract class SpotService : BinanceService
    {
        protected const string DEFAULT_SPOT_BASE_URL = "https://api.binance.com";

        public SpotService(HttpClient httpClient, string apiKey, string apiSecret, string baseUrl = "https://api.binance.com")
            : base(httpClient, baseUrl, apiKey, apiSecret)
        {
        }

        public SpotService(HttpClient httpClient, string apiKey, IBinanceSignatureService signatureService, string baseUrl = "https://api.binance.com")
            : base(httpClient, baseUrl, apiKey, signatureService)
        {
        }
    }
}