using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Common
{
    //
    // Summary:
    //     Binance exception class for any errors throw as a result of communication via
    //     http.
    public class BinanceHttpException : Exception
    {
        public int StatusCode { get; set; }

        public Dictionary<string, IEnumerable<string>> Headers { get; set; }

        public BinanceHttpException()
        {
        }

        public BinanceHttpException(string message)
            : base(message)
        {
        }

        public BinanceHttpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
