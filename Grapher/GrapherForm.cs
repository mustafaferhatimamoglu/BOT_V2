using BOT_V2.Indicators;
using BOT_V2.Operations;
using ScottPlot;
using ScottPlot.Plottable;
using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOT_V2.Grapher
{
    public partial class GrapherForm : Form
    {
        FormsPlot[] FormsPlots;
        SignalPlotXY coinPrice_High = new();
        SignalPlotXY coinPrice_Low = new();
        string[] intervals = { 
            "1m", "5m", "15m", "30m", "1h", 
            "2h", "4h", "6h", "8h", "12h",
            "1d", "3d", "1w" };

        string coinName;
        public GrapherForm(string coinName)
        {
            InitializeComponent();
            this.coinName = coinName;

            FormsPlots = new FormsPlot[] { FP_CoinPrice };
            foreach (var fp in FormsPlots)
            {
                fp.AxesChanged += FP_CoinPrice_AxesChanged;
                fp.Plot.Style(Style.Black);
                fp.Plot.XAxis.DateTimeFormat(true);
            }
            FP_CoinPrice.Load += FP_CoinPrice_Load;
            FP_CoinPrice.MouseEnter += FP_CoinPrice_MouseEnter;
            FP_CoinPrice.MouseLeave += FP_CoinPrice_MouseLeave;
            FP_CoinPrice.MouseMove += FP_CoinPrice_MouseMove;
            //B_NEXT.Click += B_NEXT_Click;
        }

        private void FP_CoinPrice_MouseMove(object? sender, MouseEventArgs e)
        {
            (double coordinateX, double coordinateY) = FP_CoinPrice.GetMouseCoordinates();
            Crosshair.X = coordinateX;
            Crosshair.Y = coordinateY;
            FP_CoinPrice.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }

        private void FP_CoinPrice_MouseLeave(object? sender, EventArgs e)
        {
            Crosshair.IsVisible = false;
            FP_CoinPrice.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }

        private Crosshair Crosshair;
        private void FP_CoinPrice_MouseEnter(object? sender, EventArgs e)
        {
            Crosshair.IsVisible = true;
        }

        private void FP_CoinPrice_Load(object? sender, EventArgs e)
        {
            Crosshair = FP_CoinPrice.Plot.AddCrosshair(0, 0);
            FP_CoinPrice_MouseLeave(null, null);
            Invoke(new Action(delegate ()
            {
                for (int CycleCount = 0; CycleCount < intervals.Count(); CycleCount++)
                {

                }
            }
            //Invoke(new Action(delegate ()
            //{
            //    string sqlQuery;
            //    List<StockData> sqlData;

            //    sqlQuery = "" +
            //        "select * from " + coinName + "_1m" + " \r\n" +
            //        "order by Kline_open_time ";
            //    sqlData = Database.GetStockDataFromDatabase_V2(sqlQuery);
            //    double[] time_1m = new double[sqlData.Count];
            //    double[] coinPrice_1m_High = new double[sqlData.Count];
            //    double[] coinPrice_1m_Low = new double[sqlData.Count];

            //    for (int i = 0; i < sqlData.Count; i++)
            //    {
            //        //a1[i] = sqlData[i].Kline_close_time;
            //        time_1m[i] = (COMMON.BinanceAuxiliary.BinanceTimeStampToUtcDateTime(sqlData[i].Kline_close_time)).ToOADate();
            //        coinPrice_1m_High[i] = sqlData[i].High_price;
            //        coinPrice_1m_Low[i] = sqlData[i].Low_price;
            //    }
            //    coinPrice_High = formsPlot1.Plot.AddSignalXY(time_1m, coinPrice_1m_High, color: Color.Green);
            //    coinPrice_Low = formsPlot1.Plot.AddSignalXY(time_1m, coinPrice_1m_Low, color: Color.Red);
            //    RefreshAllPlots();
            //    //-----------------------------------------------------------------------------------------------------------------------------
            //    sqlQuery = "" +
            //        "select * from " + coinName + "_" + indicator_Interval + "_RSI" + " \r\n" +
            //        "order by time ";
            //    //sqlData = Database.GetStockDataFromDatabase_V3(sqlQuery);
            //    sqlData = Database.GetStockDataFromDatabase_RSI(sqlQuery);
            //    double[] time_RSI = new double[sqlData.Count];
            //    double[] RSI_6 = new double[sqlData.Count];
            //    double[] RSI_12 = new double[sqlData.Count];
            //    double[] RSI_24 = new double[sqlData.Count];

            //    for (int i = 0; i < sqlData.Count; i++)
            //    {
            //        //a1[i] = sqlData[i].Kline_close_time;
            //        time_RSI[i] = (COMMON.BinanceAuxiliary.BinanceTimeStampToUtcDateTime(sqlData[i].TIME)).ToOADate();
            //        RSI_6[i] = sqlData[i].RSI_6;
            //        RSI_12[i] = sqlData[i].RSI_12;
            //        RSI_24[i] = sqlData[i].RSI_24;
            //    }
            //    this.RSI_6 = formsPlot2.Plot.AddSignalXY(time_RSI, RSI_6, color: Color.Green);
            //    this.RSI_12 = formsPlot2.Plot.AddSignalXY(time_RSI, RSI_12, color: Color.Red);
            //    this.RSI_24 = formsPlot2.Plot.AddSignalXY(time_RSI, RSI_24, color: Color.Red);
            //    RefreshAllPlots();
            //    //-----------------------------------------------------------------------------------------------------------------------------
            //    sqlQuery = "" +
            //        "select * from " + coinName + "_" + indicator_Interval + "_KDJ" + " \r\n" +
            //        "order by time ";
            //    sqlData = Database.GetStockDataFromDatabase_V4(sqlQuery);
            //    double[] time_KDJ = new double[sqlData.Count];
            //    double[] KDJ_K = new double[sqlData.Count];
            //    double[] KDJ_D = new double[sqlData.Count];
            //    double[] KDJ_J = new double[sqlData.Count];

            //    for (int i = 0; i < sqlData.Count; i++)
            //    {
            //        time_KDJ[i] = (COMMON.BinanceAuxiliary.BinanceTimeStampToUtcDateTime(sqlData[i].TIME)).ToOADate();
            //        KDJ_K[i] = sqlData[i].K;
            //        KDJ_D[i] = sqlData[i].D;
            //        KDJ_J[i] = sqlData[i].J;
            //    }
            //    this.KDJ_K = formsPlot3.Plot.AddSignalXY(time_KDJ, KDJ_K, color: Color.Yellow);
            //    this.KDJ_D = formsPlot3.Plot.AddSignalXY(time_KDJ, KDJ_D, color: Color.Red);
            //    this.KDJ_J = formsPlot3.Plot.AddSignalXY(time_KDJ, KDJ_J, color: Color.Purple);
            //    RefreshAllPlots();
            //    //-----------------------------------------------------------------------------------------------------------------------------
            //    string sqlQuery_1h_1m = "" +
            //    "select minute.Kline_close_time, minute.Low_price, minute.High_price\r\n" +
            //    "from [" + coinName + "_1h] as hour inner join [" + coinName + "_1m] as minute\r\n" +
            //    "on hour.Kline_close_time = minute.Kline_close_time \r\n" +
            //    "order by minute.Kline_open_time  ";
            //    DataTable sqlQuery_1h_1m_dt = Database.SQL_query(sqlQuery_1h_1m);
            //    double[] time_1h_1m = new double[sqlQuery_1h_1m_dt.Rows.Count];
            //    double[] coinPrice_1h_1m_High = new double[sqlQuery_1h_1m_dt.Rows.Count];
            //    double[] coinPrice_1h_1m_Low = new double[sqlQuery_1h_1m_dt.Rows.Count];
            //    for (int i = 0; i < sqlQuery_1h_1m_dt.Rows.Count; i++)
            //    {
            //        time_1h_1m[i] = (COMMON.BinanceAuxiliary.BinanceTimeStampToUtcDateTime((Int64)sqlQuery_1h_1m_dt.Rows[i]["Kline_close_time"])).ToOADate();
            //        coinPrice_1h_1m_High[i] = Convert.ToDouble(sqlQuery_1h_1m_dt.Rows[i]["High_price"]);
            //        coinPrice_1h_1m_Low[i] = Convert.ToDouble(sqlQuery_1h_1m_dt.Rows[i]["Low_price"]);
            //    }
            //    for (int i = 15; i < time_RSI.Count() - 10; i++)
            //    {
            //        if (
            //        KDJ_D[i] > KDJ_K[i] + _KDJ
            //        && KDJ_K[i] > KDJ_J[i] + _KDJ
            //        && RSI_6[i] > _RSI_t1
            //        && RSI_6[i - 1] < _RSI_t0
            //        //&& RSI_24[i] > RSI_12[i] && RSI_12[i] > RSI_6[i]
            //        )
            //        {
            //            HotSpots_X.Add(time_RSI[i]);
            //            HotSpots_Y.Add(coinPrice_1h_1m_High[i]);
            //        }
            //    }
            //    for (int i = 0; i < HotSpots_X.Count; i++)
            //    {
            //        int t1 = Array.IndexOf(time_1m, HotSpots_X[i]);
            //        for (int j = t1; j < time_1m.Count(); j++)
            //        {
            //            if (HotSpots_Y[i] * _Short >= coinPrice_1m_Low[j])
            //            {
            //                HotSpots_X_end.Add(time_1m[j]);
            //                HotSpots_Y_end.Add(coinPrice_1m_Low[j]);
            //                HotSpots_end_status.Add(0x02);
            //                break;
            //            }
            //            else if (HotSpots_Y[i] * _Long <= coinPrice_1m_High[j])
            //            {
            //                HotSpots_X_end.Add(time_1m[j]);
            //                HotSpots_Y_end.Add(coinPrice_1m_High[j]);
            //                HotSpots_end_status.Add(0x01);
            //                break;
            //            }
            //        }
            //    }
            //    for (int i = 0; i < HotSpots_X_end.Count; i++)
            //    {
            //        var rp = formsPlot1.Plot.AddRectangle(
            //            xMin: HotSpots_X[i], xMax: HotSpots_X_end[i], yMin: HotSpots_Y[i], yMax: HotSpots_Y_end[i]);
            //        rp.BorderColor = Color.Blue;
            //        rp.BorderLineWidth = 3;
            //        rp.BorderLineStyle = LineStyle.Dot;
            //        if (HotSpots_end_status[i] == 0x01)
            //        {
            //            rp.Color = Color.FromArgb(100, Color.Green);
            //        }
            //        else
            //        {
            //            rp.Color = Color.FromArgb(100, Color.Red);
            //        }

            //    }
            //    foreach (var VerticalLine_X in HotSpots_X)
            //    {
            //        DrawVerticalLine_All(VerticalLine_X);
            //    }
            //    int SuccessCount = 0;
            //    int FailCount = 0;

            //    foreach (var status in HotSpots_end_status)
            //    {
            //        if (status == 0x01)
            //            SuccessCount++;
            //        else if (status == 0x02)
            //            FailCount++;
            //    }
            //    B_Success.Text = SuccessCount.ToString();
            //    B_Fail.Text = FailCount.ToString();
            //    RefreshAllPlots();
            //}));
        }

        private void FP_CoinPrice_AxesChanged(object? sender, EventArgs e)
        {
            foreach (var fp in FormsPlots)
            {
                if (fp == FP_CoinPrice)
                    continue;
                //if (fp == formsPlot2)
                //{
                //    var newAxisLimits = formsPlot1.Plot.GetAxisLimits();
                //    var x_newAxisLimits = formsPlot1.Plot.GetAxisLimits(0, 1);
                //    x_newAxisLimits = x_newAxisLimits.WithY(0, 100);
                //    // disable events briefly to avoid an infinite loop
                //    fp.Configuration.AxesChangedEventEnabled = false;
                //    fp.Plot.SetAxisLimits(x_newAxisLimits);
                //    fp.Render();
                //    fp.Configuration.AxesChangedEventEnabled = true;
                //}
                //if (fp == formsPlot3)
                //{
                //    var newAxisLimits = formsPlot1.Plot.GetAxisLimits();
                //    var x_newAxisLimits = formsPlot1.Plot.GetAxisLimits(0, 1);
                //    x_newAxisLimits = x_newAxisLimits.WithY(-20, 120);
                //    // disable events briefly to avoid an infinite loop
                //    fp.Configuration.AxesChangedEventEnabled = false;
                //    fp.Plot.SetAxisLimits(x_newAxisLimits);
                //    fp.Render();
                //    fp.Configuration.AxesChangedEventEnabled = true;
                //}
            }
        }
    }
}
