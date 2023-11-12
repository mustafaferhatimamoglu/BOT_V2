using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Xml.Linq;

namespace BOT_V2.MachineLearning
{
    public class Coin
    {
        [LoadColumn(0)] public float Kline_open_time;
        [LoadColumn(1)] public float Open_price;
        [LoadColumn(2)] public float High_price;
        [LoadColumn(3)] public float Low_price;
        [LoadColumn(4)] public float Close_price;
        [LoadColumn(5)] public float Volume;
        [LoadColumn(6)] public float Kline_close_time;
        [LoadColumn(7)] public float Quote_asset_volume;
        [LoadColumn(8)] public float Number_of_trades;
        [LoadColumn(9)] public float Taker_buy_base_asset_volume;
        [LoadColumn(10)] public float Taker_buy_quote_asset_volume;
        [LoadColumn(11)] public float RSI_6;
        [LoadColumn(12)] public float RSI_12;
        [LoadColumn(13)] public float RSI_24;
        [LoadColumn(14)] public float KDJ_K;
        [LoadColumn(15)] public float KDJ_D;
        [LoadColumn(16)] public float KDJ_J;
    }

    public class CoinPrediction
    {
        [ColumnName("Score")]
        public float Price;
    }
    public partial class ML_V1 : Form
    {
        public ML_V1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT TOP 1000\r\n  BTCUSDT_1h.*\r\n" +
                "  , RSI_6, RSI_12, RSI_24\r\n  , K, D, J\r\nFROM BTCUSDT_1h\r\n" +
                "  inner join BTCUSDT_1h_RSI on \r\n  BTCUSDT_1h.Kline_close_time = BTCUSDT_1h_RSI.Time\r\n" +
                "  inner join BTCUSDT_1h_KDJ on \r\n  BTCUSDT_1h.Kline_close_time = BTCUSDT_1h_KDJ.Time\r\n" +
                "order by Kline_close_time;";
            var sql_Data = Operations.KlineData.GetKlineData_All(sqlQuery);



            MLContext mlContext = new MLContext();
            var data = new List<Coin>();
            var trainingDataView = mlContext.Data.LoadFromEnumerable(data);

            //IDataView trainingDataView = mlContext.Data.LoadFromTextFile<Coin>("YourData.csv", hasHeader: true, separatorChar: ',');
            //IDataView trainingDataView = mlContext.Data.L
            //mlContext.Data.

            //        var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Close_price")
            //.Append(mlContext.Transforms.Concatenate("Features", "Kline_open_time", "Open_price", "High_price", "Low_price", "Volume", "Kline_close_time", "Quote_asset_volume", "Number_of_trades", "Taker_buy_base_asset_volume", "Taker_buy_quote_asset_volume", "RSI_6", "RSI_12", "RSI_24", "KDJ_K", "KDJ_D", "KDJ_J"))
            //.Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features"))
            //.Append(mlContext.Transforms.CopyColumns(outputColumnName: "Score", inputColumnName: "Score"));
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Close_price")
            .Append(mlContext.Transforms.Concatenate("Features", "Kline_open_time", "Open_price", "High_price", "Low_price", "Volume", "Kline_close_time", "Quote_asset_volume", "Number_of_trades", "Taker_buy_base_asset_volume", "Taker_buy_quote_asset_volume", "RSI_6", "RSI_12", "RSI_24", "KDJ_K", "KDJ_D", "KDJ_J"))
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features"))
            .Append(mlContext.Transforms.CopyColumns(outputColumnName: "Score", inputColumnName: "Score"));

            
            foreach (var _Data in sql_Data)
            {
                data.Add(new Coin {
                    Kline_open_time = _Data.Kline_open_time,
                    Open_price = (float)_Data.Open_price,
                    High_price = (float)_Data.High_price,
                    Low_price = (float)_Data.Low_price,
                    Close_price = (float)_Data.Close_price,
                    Volume = (float)_Data.Volume,
                    Kline_close_time = _Data.Kline_close_time,
                    Quote_asset_volume = (float)_Data.Quote_asset_volume,
                    Number_of_trades = (float)_Data.Number_of_trades,
                    Taker_buy_base_asset_volume = (float)_Data.Taker_buy_base_asset_volume,
                    Taker_buy_quote_asset_volume = (float)_Data.Taker_buy_quote_asset_volume,
                    RSI_6 = (float)_Data.RSI_6,
                    RSI_12 = (float)_Data.RSI_12,
                    RSI_24 = (float)_Data.RSI_24,
                    KDJ_K = (float)_Data.KDJ_K,
                    KDJ_D = (float)_Data.KDJ_D,
                    KDJ_J = (float)_Data.KDJ_J,
                });                
            }
            var model = pipeline.Fit(trainingDataView);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<Coin, CoinPrediction>(model);
            //break;

            string sqlQuery_2 = " SELECT --TOP 100\r\n" +
                "  BTCUSDT_1h.*\r\n  ,RSI_6,RSI_12,RSI_24\r\n  ,K,D,J\r\n" +
                "  FROM BTCUSDT_1h \r\n" +
                "  inner join BTCUSDT_1h_RSI on \r\n  BTCUSDT_1h.Kline_close_time = BTCUSDT_1h_RSI.Time\r\n" +
                "  inner join BTCUSDT_1h_KDJ on \r\n  BTCUSDT_1h.Kline_close_time = BTCUSDT_1h_KDJ.Time\r\n" +
                " order by Kline_close_time\r\noffset 1000 ROWS\r\nFETCH NEXT 10 ROWS ONLY";
            var sql_Data_2 = Operations.KlineData.GetKlineData_All(sqlQuery_2);

            foreach (var _Data in sql_Data_2)
            {
                data.Add(new Coin
                {
                    Kline_open_time = _Data.Kline_open_time,
                    Open_price = (float)_Data.Open_price,
                    High_price = (float)_Data.High_price,
                    Low_price = (float)_Data.Low_price,
                    Close_price = (float)_Data.Close_price,
                    Volume = (float)_Data.Volume,
                    Kline_close_time = _Data.Kline_close_time,
                    Quote_asset_volume = (float)_Data.Quote_asset_volume,
                    Number_of_trades = (float)_Data.Number_of_trades,
                    Taker_buy_base_asset_volume = (float)_Data.Taker_buy_base_asset_volume,
                    Taker_buy_quote_asset_volume = (float)_Data.Taker_buy_quote_asset_volume,
                    RSI_6 = (float)_Data.RSI_6,
                    RSI_12 = (float)_Data.RSI_12,
                    RSI_24 = (float)_Data.RSI_24,
                    KDJ_K = (float)_Data.KDJ_K,
                    KDJ_D = (float)_Data.KDJ_D,
                    KDJ_J = (float)_Data.KDJ_J,
                });

                var coinSample = new Coin() { Kline_open_time = _Data.Kline_close_time + 1 };
                var prediction = predictionEngine.Predict(coinSample);
            }



            //var prediction = predictionEngine.Predict(coinSample);

            //Console.WriteLine($"Predicted coin price for date {coinSample.Date}: {prediction.Price}");
        }
    }
}
