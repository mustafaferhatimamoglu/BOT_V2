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

namespace BOT_V2.MachineLearning
{
    public class Coin
    {
        [LoadColumn(0)] public float Price;
        [LoadColumn(1)] public float Date;
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
            string sqlQuery = "SELECT TOP 100\r\n  BTCUSDT_1h.*\r\n" +
                "  , RSI_6, RSI_12, RSI_24\r\n  , K, D, J\r\nFROM BTCUSDT_1h\r\n" +
                "  inner join BTCUSDT_1h_RSI on \r\n  BTCUSDT_1h.Kline_close_time = BTCUSDT_1h_RSI.Time\r\n" +
                "  inner join BTCUSDT_1h_KDJ on \r\n  BTCUSDT_1h.Kline_close_time = BTCUSDT_1h_KDJ.Time\r\n" +
                "order by Kline_close_time;";
            var _Data = Operations.KlineData.GetKlineData_All(sqlQuery);



            MLContext mlContext = new MLContext();

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<Coin>("YourData.csv", hasHeader: true, separatorChar: ',');

            //mlContext.Data.

            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Price")
                .Append(mlContext.Transforms.Concatenate("Features", "Date"))
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features"))
                .Append(mlContext.Transforms.CopyColumns(outputColumnName: "Score", inputColumnName: "Score"));

            var model = pipeline.Fit(trainingDataView);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<Coin, CoinPrediction>(model);

            var coinSample = new Coin() { Date = 0.043f };

            var prediction = predictionEngine.Predict(coinSample);

            Console.WriteLine($"Predicted coin price for date {coinSample.Date}: {prediction.Price}");
        }
    }
}
