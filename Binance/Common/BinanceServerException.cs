using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Common
{
    //
    // Summary:
    //     Binance exception class for any errors throw as a result internal server issues.
    public class BinanceServerException : BinanceHttpException
    {
        public new string Message { get; protected set; }

        public BinanceServerException()
        {
        }

        public BinanceServerException(string message)
            : base(message)
        {
            Message = message;
        }

        public BinanceServerException(string message, Exception innerException)
            : base(message, innerException)
        {
            Message = message;
        }
    }
}