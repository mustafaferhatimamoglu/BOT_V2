using BOT_V2.Binance.Common;
using BOT_V2.Binance.Spot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Spot
{
    public class Market : SpotService
    {
        private const string TEST_CONNECTIVITY = "/api/v3/ping";

        private const string CHECK_SERVER_TIME = "/api/v3/time";

        private const string EXCHANGE_INFORMATION = "/api/v3/exchangeInfo";

        private const string ORDER_BOOK = "/api/v3/depth";

        private const string RECENT_TRADES_LIST = "/api/v3/trades";

        private const string OLD_TRADE_LOOKUP = "/api/v3/historicalTrades";

        private const string COMPRESSED_AGGREGATE_TRADES_LIST = "/api/v3/aggTrades";

        private const string KLINE_CANDLESTICK_DATA = "/api/v3/klines";

        private const string CURRENT_AVERAGE_PRICE = "/api/v3/avgPrice";

        private const string TWENTY_FOUR_HR_TICKER_PRICE_CHANGE_STATISTICS = "/api/v3/ticker/24hr";

        private const string SYMBOL_PRICE_TICKER = "/api/v3/ticker/price";

        private const string SYMBOL_ORDER_BOOK_TICKER = "/api/v3/ticker/bookTicker";

        private const string ROLLING_WINDOW_PRICE_CHANGE_STATISTICS = "/api/v3/ticker";

        public Market(string baseUrl = "https://api.binance.com", string apiKey = null, string apiSecret = null)
            : this(new HttpClient(), baseUrl, apiKey, apiSecret)
        {
        }

        public Market(HttpClient httpClient, string baseUrl = "https://api.binance.com", string apiKey = null, string apiSecret = null)
            : base(httpClient, apiKey, apiSecret, baseUrl)
        {
        }

        public Market(HttpClient httpClient, IBinanceSignatureService signatureService, string baseUrl = "https://api.binance.com", string apiKey = null)
            : base(httpClient, apiKey, signatureService, baseUrl)
        {
        }

        //
        // Summary:
        //     Test connectivity to the Rest API.
        //     Weight(IP): 1.
        //
        // Returns:
        //     OK.
        public async Task<string> TestConnectivity()
        {
            return await SendPublicAsync<string>("/api/v3/ping", HttpMethod.Get);
        }

        //
        // Summary:
        //     Test connectivity to the Rest API and get the current server time.
        //     Weight(IP): 1.
        //
        // Returns:
        //     Binance server UTC timestamp.
        public async Task<string> CheckServerTime()
        {
            return await SendPublicAsync<string>("/api/v3/time", HttpMethod.Get);
        }

        //
        // Summary:
        //     Current exchange trading rules and symbol information.
        //     - If any symbol provided in either symbol or symbols do not exist, the endpoint
        //     will throw an error.
        //     Weight(IP): 10.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   symbols:
        //
        // Returns:
        //     Current exchange trading rules and symbol information.
        public async Task<string> ExchangeInformation(string symbol = null, string symbols = null)
        {
            return await SendPublicAsync<string>("/api/v3/exchangeInfo", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "symbols", symbols }
            });
        }

        //
        // Summary:
        //     | Limit | Weight(IP) |.
        //     |---------------------|-------------|.
        //     | 1-100 | 1 |.
        //     | 101-500 | 5 |.
        //     | 501-1000 | 10 |.
        //     | 1001-5000 | 50 |.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   limit:
        //     If limit > 5000, then the response will truncate to 5000.
        //
        // Returns:
        //     Order book.
        public async Task<string> OrderBook(string symbol, int? limit = null)
        {
            return await SendPublicAsync<string>("/api/v3/depth", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "limit", limit }
            });
        }

        //
        // Summary:
        //     Get recent trades.
        //     Weight(IP): 1.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   limit:
        //     Default 500; max 1000.
        //
        // Returns:
        //     Trade list.
        public async Task<string> RecentTradesList(string symbol, int? limit = null)
        {
            return await SendPublicAsync<string>("/api/v3/trades", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "limit", limit }
            });
        }

        //
        // Summary:
        //     Get older market trades.
        //     Weight(IP): 5.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   limit:
        //     Default 500; max 1000.
        //
        //   fromId:
        //     Trade id to fetch from. Default gets most recent trades.
        //
        // Returns:
        //     Trade list.
        public async Task<string> OldTradeLookup(string symbol, int? limit = null, long? fromId = null)
        {
            return await SendPublicAsync<string>("/api/v3/historicalTrades", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "limit", limit },
                { "fromId", fromId }
            });
        }

        //
        // Summary:
        //     Get compressed, aggregate trades. Trades that fill at the time, from the same
        //     order, with the same price will have the quantity aggregated.
        //     - If `startTime` and `endTime` are sent, time between startTime and endTime must
        //     be less than 1 hour.
        //     - If `fromId`, `startTime`, and `endTime` are not sent, the most recent aggregate
        //     trades will be returned.
        //     Weight(IP): 1.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   fromId:
        //     Trade id to fetch from. Default gets most recent trades.
        //
        //   startTime:
        //     UTC timestamp in ms.
        //
        //   endTime:
        //     UTC timestamp in ms.
        //
        //   limit:
        //     Default 500; max 1000.
        //
        // Returns:
        //     Trade list.
        public async Task<string> CompressedAggregateTradesList(string symbol, long? fromId = null, long? startTime = null, long? endTime = null, int? limit = null)
        {
            return await SendPublicAsync<string>("/api/v3/aggTrades", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "fromId", fromId },
                { "startTime", startTime },
                { "endTime", endTime },
                { "limit", limit }
            });
        }

        //
        // Summary:
        //     Kline/candlestick bars for a symbol.
        //     Klines are uniquely identified by their open time.
        //     - If `startTime` and `endTime` are not sent, the most recent klines are returned.
        //     Weight(IP): 1.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   interval:
        //     kline intervals.
        //
        //   startTime:
        //     UTC timestamp in ms.
        //
        //   endTime:
        //     UTC timestamp in ms.
        //
        //   limit:
        //     Default 500; max 1000.
        //
        // Returns:
        //     Kline data.
        public async Task<string> KlineCandlestickData(string symbol, Interval interval, long? startTime = null, long? endTime = null, int? limit = null)
        {
            return await SendPublicAsync<string>("/api/v3/klines", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "interval", interval },
                { "startTime", startTime },
                { "endTime", endTime },
                { "limit", limit }
            });
        }

        //
        // Summary:
        //     Current average price for a symbol.
        //     Weight(IP): 1.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        // Returns:
        //     Average price.
        public async Task<string> CurrentAveragePrice(string symbol)
        {
            return await SendPublicAsync<string>("/api/v3/avgPrice", HttpMethod.Get, new Dictionary<string, object> { { "symbol", symbol } });
        }

        //
        // Summary:
        //     24 hour rolling window price change statistics. Careful when accessing this with
        //     no symbol.
        //     - If the symbol is not sent, tickers for all symbols will be returned in an array.
        //     Weight(IP):.
        //     - `1` for a single symbol;.
        //     - `40` when the symbol parameter is omitted;.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   symbols:
        //
        // Returns:
        //     24hr ticker.
        public async Task<string> TwentyFourHrTickerPriceChangeStatistics(string symbol = null, string symbols = null)
        {
            return await SendPublicAsync<string>("/api/v3/ticker/24hr", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "symbols", symbols }
            });
        }

        //
        // Summary:
        //     Latest price for a symbol or symbols.
        //     - If the symbol is not sent, prices for all symbols will be returned in an array.
        //     Weight(IP):.
        //     - `1` for a single symbol;.
        //     - `2` when the symbol parameter is omitted;.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   symbols:
        //
        // Returns:
        //     Price ticker.
        public async Task<string> SymbolPriceTicker(string symbol = null, string symbols = null)
        {
            return await SendPublicAsync<string>("/api/v3/ticker/price", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "symbols", symbols }
            });
        }

        //
        // Summary:
        //     Best price/qty on the order book for a symbol or symbols.
        //     - If the symbol is not sent, bookTickers for all symbols will be returned in
        //     an array.
        //     Weight(IP):.
        //     - `1` for a single symbol;.
        //     - `2` when the symbol parameter is omitted;.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   symbols:
        //
        // Returns:
        //     Order book ticker.
        public async Task<string> SymbolOrderBookTicker(string symbol = null, string symbols = null)
        {
            return await SendPublicAsync<string>("/api/v3/ticker/bookTicker", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "symbols", symbols }
            });
        }

        //
        // Summary:
        //     The window used to compute statistics is typically slightly wider than requested
        //     windowSize.
        //     openTime for /api/v3/ticker always starts on a minute, while the closeTime is
        //     the current time of the request. As such, the effective window might be up to
        //     1 minute wider than requested.
        //     E.g. If the closeTime is 1641287867099 (January 04, 2022 09:17:47:099 UTC) ,
        //     and the windowSize is 1d. the openTime will be: 1641201420000 (January 3, 2022,
        //     09:17:00 UTC).
        //     Weight(IP): 2 for each requested symbol regardless of windowSize.
        //     The weight for this request will cap at 100 once the number of symbols in the
        //     request is more than 50.
        //
        // Parameters:
        //   symbol:
        //     Trading symbol, e.g. BNBUSDT.
        //
        //   symbols:
        //     Either symbol or symbols must be provided.
        //     Examples of accepted format for the symbols parameter: ["BTCUSDT","BNBUSDT"]
        //     or %5B%22BTCUSDT%22,%22BNBUSDT%22%5D.
        //     The maximum number of symbols allowed in a request is 100.
        //
        //   windowSize:
        //     Defaults to 1d if no parameter provided.
        //     Supported windowSize values:.
        //     1m,2m....59m for minutes.
        //     1h, 2h....23h - for hours.
        //     1d...7d - for days.
        //     Units cannot be combined (e.g. 1d2h is not allowed).
        //
        // Returns:
        //     Rolling price ticker.
        public async Task<string> RollingWindowPriceChangeStatistics(string symbol = null, string symbols = null, string windowSize = null)
        {
            return await SendPublicAsync<string>("/api/v3/ticker", HttpMethod.Get, new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "symbols", symbols },
                { "windowSize", windowSize }
            });
        }
    }
}