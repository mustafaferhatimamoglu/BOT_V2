using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Common
{
    //
    // Summary:
    //     Binance exception class for any errors throw as a result of the misuse of the
    //     API or the library.
    public class BinanceClientException : BinanceHttpException
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public new string Message { get; protected set; }

        public BinanceClientException()
        {
        }

        public BinanceClientException(string message, int code)
            : base(message)
        {
            Code = code;
            Message = message;
        }

        public BinanceClientException(string message, int code, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            Message = message;
        }
    }
}