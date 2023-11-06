using BOT_V2.Binance.Spot.Models;
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
    public partial class GrapherForm_V4 : Form
    {
        FormsPlot[] FormsPlots;
        //SignalPlotXY coinPrice_High = new();
        //SignalPlotXY coinPrice_Low = new(); 
        SignalPlotXY[] coinPrice_High = new SignalPlotXY[13];
        SignalPlotXY[] coinPrice_Low = new SignalPlotXY[13];

        SignalPlotXY[] Indicator_RSI_6 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_RSI_12 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_RSI_24 = new SignalPlotXY[13];

        SignalPlotXY[] Indicator_KDJ_K = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_KDJ_D = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_KDJ_J = new SignalPlotXY[13];

        SignalPlotXY[] Indicator_OBV_6 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_OBV_12 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_OBV_24 = new SignalPlotXY[13];
        //double[][][] d_coinPrice = new double[3][][];
        double[,][] d_coinPrice;
        double[,][] d_indicator_RSI;
        double[,][] d_indicator_KDJ;
        double[,][] d_indicator_OBV;
        string[] intervals = {
            "1m", "5m", "15m", "30m", "1h",
            "2h", "4h", "6h", "8h", "12h",
            "1d", "3d", "1w" };

        string coinName;
        int interval_position;
        double RSI_limit;
        double getProfit;
        double stopLoss;
        int stepSize = 3;
        bool noGraph;
        public GrapherForm_V4(string coinName, int interval_position, double RSI_limit, double getProfit, double stopLoss)
        {
            InitializeComponent();
            this.coinName = coinName;
            this.interval_position = interval_position;
            this.RSI_limit = RSI_limit;
            this.getProfit = getProfit;
            this.stopLoss = stopLoss;
            this.noGraph = false;


            FormsPlots = new FormsPlot[] { FP_CoinPrice, FP_RSI, FP_KDJ, FP_OBV };
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
            Create_CoinPrice_Buttons();
        }
        public GrapherForm_V4(string coinName, bool noGraph)
        {
            InitializeComponent();
            this.coinName = coinName;
            this.noGraph = noGraph;
            FP_CoinPrice.Load += FP_CoinPrice_Load;
        }
        #region R
        void Create_CoinPrice_Buttons()
        {
            for (int i = 0; i < intervals.Count(); i++)
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_CoinPrice_" + i.ToString(),
                    Text = intervals[i],

                });
                ((Button)TLP_Buttons.Controls.Find("Button_CoinPrice_" + i.ToString(), true).First()).Click += Button_CoinPrice_Clicked;
            }
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_Back",
                    Text = "Back",

                });
                ((Button)TLP_Buttons.Controls.Find("Button_Back", true).First()).Click += Button_Back_Clicked;
            }
            for (int i = 0; i < intervals.Count(); i++)
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_CoinIndicator_" + i.ToString(),
                    Text = intervals[i],

                });
                ((Button)TLP_Buttons.Controls.Find("Button_CoinIndicator_" + i.ToString(), true).First()).Click += Button_CoinIndicator_Clicked;
            }
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_Next",
                    Text = "Next",

                });
                ((Button)TLP_Buttons.Controls.Find("Button_Next", true).First()).Click += Button_Next_Clicked;
            }
        }
        void DrawVerticalLine_All(double gelen)
        {
            foreach (var item in FormsPlots)
            {
                item.Plot.AddVerticalLine(gelen);
            }
        }
        private void Button_Next_Clicked(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void Button_Back_Clicked(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void Button_CoinIndicator_Clicked(object? sender, EventArgs e)
        {
            Button rx = (Button)sender;
            int rxi = Convert.ToInt32(rx.Name.Split('_')[2]);
            Indicator_RSI_6[rxi].IsVisible = !Indicator_RSI_6[rxi].IsVisible;
            Indicator_RSI_12[rxi].IsVisible = !Indicator_RSI_12[rxi].IsVisible;
            Indicator_RSI_24[rxi].IsVisible = !Indicator_RSI_24[rxi].IsVisible;

            Indicator_KDJ_K[rxi].IsVisible = !Indicator_KDJ_K[rxi].IsVisible;
            Indicator_KDJ_D[rxi].IsVisible = !Indicator_KDJ_D[rxi].IsVisible;
            Indicator_KDJ_J[rxi].IsVisible = !Indicator_KDJ_J[rxi].IsVisible;

            Indicator_OBV_6[rxi].IsVisible = !Indicator_OBV_6[rxi].IsVisible;
            Indicator_OBV_12[rxi].IsVisible = !Indicator_OBV_12[rxi].IsVisible;
            Indicator_OBV_24[rxi].IsVisible = !Indicator_OBV_24[rxi].IsVisible;
            if (Indicator_RSI_6[rxi].IsVisible)
            {
                rx.BackColor = Color.Green;
            }
            else
            {
                rx.BackColor = Color.Empty;
            }
        }
        void Button_CoinPrice_Clicked(object? sender, EventArgs e)
        {
            Button rx = (Button)sender;
            int rxi = Convert.ToInt32(rx.Name.Split('_')[2]);
            coinPrice_High[rxi].IsVisible = !coinPrice_High[rxi].IsVisible;
            coinPrice_Low[rxi].IsVisible = !coinPrice_Low[rxi].IsVisible;
            if (coinPrice_High[rxi].IsVisible)
            {
                rx.BackColor = Color.Green;
            }
            else
            {
                rx.BackColor = Color.Empty;
            }
        }
        private void FP_CoinPrice_MouseMove(object? sender, MouseEventArgs e)
        {
            (double coordinateX, double coordinateY) = FP_CoinPrice.GetMouseCoordinates();
            Crosshair.X = coordinateX;
            Crosshair.Y = coordinateY;

            foreach (var item in Crosshair_Indicators)
            {
                item.X = coordinateX;
            }
            for (int i = 1; i < FormsPlots.Count(); i++)
            {
                FormsPlots[i].Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
            }
            FP_CoinPrice.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }
        private void FP_CoinPrice_MouseLeave(object? sender, EventArgs e)
        {
            Crosshair.IsVisible = false;
            foreach (var item in Crosshair_Indicators)
            {
                item.IsVisible = false;
            }
            for (int i = 1; i < FormsPlots.Count(); i++)
            {
                FormsPlots[i].Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
            }
            FP_CoinPrice.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }

        private Crosshair Crosshair;
        private Crosshair[] Crosshair_Indicators;//= new Crosshair[3];
        private void FP_CoinPrice_MouseEnter(object? sender, EventArgs e)
        {
            Crosshair.IsVisible = true;
            foreach (var item in Crosshair_Indicators)
            {
                item.IsVisible = true;
            }
        }
        #endregion
        private void FP_CoinPrice_Load(object? sender, EventArgs e)
        {
            #region CrossHairSetup
            if (noGraph == false)
            {
                Crosshair = FP_CoinPrice.Plot.AddCrosshair(0, 0);
                Crosshair_Indicators = new Crosshair[FormsPlots.Count() - 1];
                for (int i = 0; i < FormsPlots.Count(); i++)
                {
                    if (FormsPlots[i] == FP_CoinPrice)
                    {
                        continue;
                    }
                    else
                    {
                        Crosshair_Indicators[i - 1] = FormsPlots[i].Plot.AddCrosshair(0, 0);
                    }
                }
                FP_CoinPrice_MouseLeave(null, null);
            }
            #endregion
            Invoke(new Action(delegate ()
            {
                #region Sql2Data
                //------------------------------------------------------------------------
                d_coinPrice = new double[intervals.Count(), 3][];
                //interval
                //time-high-low
                //counter
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + " \r\n" +
                        "order by Kline_open_time ";
                    var klinePriceData = Operations.KlineData.GetKlineData(sqlQuery);
                    d_coinPrice[count_interval, 0] = new double[klinePriceData.Count];
                    d_coinPrice[count_interval, 1] = new double[klinePriceData.Count];
                    d_coinPrice[count_interval, 2] = new double[klinePriceData.Count];
                    //d_coinPrice[ count_interval, count_interval][5] = 0;

                    for (int counter_klinePriceData = 0; counter_klinePriceData < klinePriceData.Count; counter_klinePriceData++)
                    {
                        d_coinPrice[count_interval, 0][counter_klinePriceData] = Indicators.Auxiliary.BTS_DateTime(klinePriceData[counter_klinePriceData].Kline_close_time).ToOADate();
                        d_coinPrice[count_interval, 1][counter_klinePriceData] = klinePriceData[counter_klinePriceData].High_price;
                        d_coinPrice[count_interval, 2][counter_klinePriceData] = klinePriceData[counter_klinePriceData].Low_price;
                    }
                    coinPrice_High[count_interval] = FP_CoinPrice.Plot.AddSignalXY(d_coinPrice[count_interval, 0], d_coinPrice[count_interval, 1], color: Color.Green);
                    coinPrice_Low[count_interval] = FP_CoinPrice.Plot.AddSignalXY(d_coinPrice[count_interval, 0], d_coinPrice[count_interval, 2], color: Color.Red);
                    coinPrice_High[count_interval].LineWidth = 1 + count_interval;
                    coinPrice_Low[count_interval].LineWidth = 1 + count_interval;
                    coinPrice_High[count_interval].IsVisible = false;
                    coinPrice_Low[count_interval].IsVisible = false;
                }
                //------------------------------------------------------------------------
                d_indicator_RSI = new double[intervals.Count(), 4][];
                //interval
                //time-6-12-24
                //counter
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + "_RSI \r\n" +
                        "order by time ";
                    var klineIndicatorData = Operations.KlineData.GetIndicatorData_RSI(sqlQuery);
                    d_indicator_RSI[count_interval, 0] = new double[klineIndicatorData.Count];
                    d_indicator_RSI[count_interval, 1] = new double[klineIndicatorData.Count];
                    d_indicator_RSI[count_interval, 2] = new double[klineIndicatorData.Count];
                    d_indicator_RSI[count_interval, 3] = new double[klineIndicatorData.Count];
                    //d_indicator_RSI[ count_interval, count_interval][5] = 0;

                    for (int counter_klineIndicatorData = 0; counter_klineIndicatorData < klineIndicatorData.Count; counter_klineIndicatorData++)
                    {
                        d_indicator_RSI[count_interval, 0][counter_klineIndicatorData] = Indicators.Auxiliary.BTS_DateTime(klineIndicatorData[counter_klineIndicatorData].TIME).ToOADate();
                        d_indicator_RSI[count_interval, 1][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].RSI_6;
                        d_indicator_RSI[count_interval, 2][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].RSI_12;
                        d_indicator_RSI[count_interval, 3][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].RSI_24;
                    }
                    Indicator_RSI_6[count_interval] = FP_RSI.Plot.AddSignalXY(d_indicator_RSI[count_interval, 0], d_indicator_RSI[count_interval, 1], color: Color.Yellow);
                    Indicator_RSI_12[count_interval] = FP_RSI.Plot.AddSignalXY(d_indicator_RSI[count_interval, 0], d_indicator_RSI[count_interval, 2], color: Color.Red);
                    Indicator_RSI_24[count_interval] = FP_RSI.Plot.AddSignalXY(d_indicator_RSI[count_interval, 0], d_indicator_RSI[count_interval, 3], color: Color.Purple);

                    Indicator_RSI_6[count_interval].LineWidth = 1 + count_interval;
                    Indicator_RSI_12[count_interval].LineWidth = 1 + count_interval;
                    Indicator_RSI_24[count_interval].LineWidth = 1 + count_interval;

                    Indicator_RSI_6[count_interval].IsVisible = false;
                    Indicator_RSI_12[count_interval].IsVisible = false;
                    Indicator_RSI_24[count_interval].IsVisible = false;
                }
                //------------------------------------------------------------------------
                d_indicator_KDJ = new double[intervals.Count(), 4][];
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + "_KDJ \r\n" +
                        "order by time ";
                    var klineIndicatorData = Operations.KlineData.GetIndicatorData_KDJ(sqlQuery);
                    d_indicator_KDJ[count_interval, 0] = new double[klineIndicatorData.Count];
                    d_indicator_KDJ[count_interval, 1] = new double[klineIndicatorData.Count];
                    d_indicator_KDJ[count_interval, 2] = new double[klineIndicatorData.Count];
                    d_indicator_KDJ[count_interval, 3] = new double[klineIndicatorData.Count];
                    //d_indicator_KDJ[ count_interval, count_interval][5] = 0;

                    for (int counter_klineIndicatorData = 0; counter_klineIndicatorData < klineIndicatorData.Count; counter_klineIndicatorData++)
                    {
                        d_indicator_KDJ[count_interval, 0][counter_klineIndicatorData] = Indicators.Auxiliary.BTS_DateTime(klineIndicatorData[counter_klineIndicatorData].TIME).ToOADate();
                        d_indicator_KDJ[count_interval, 1][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].KDJ_K;
                        d_indicator_KDJ[count_interval, 2][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].KDJ_D;
                        d_indicator_KDJ[count_interval, 3][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].KDJ_J;
                    }
                    Indicator_KDJ_K[count_interval] = FP_KDJ.Plot.AddSignalXY(d_indicator_KDJ[count_interval, 0], d_indicator_KDJ[count_interval, 1], color: Color.Yellow);
                    Indicator_KDJ_D[count_interval] = FP_KDJ.Plot.AddSignalXY(d_indicator_KDJ[count_interval, 0], d_indicator_KDJ[count_interval, 2], color: Color.Red);
                    Indicator_KDJ_J[count_interval] = FP_KDJ.Plot.AddSignalXY(d_indicator_KDJ[count_interval, 0], d_indicator_KDJ[count_interval, 3], color: Color.Purple);

                    Indicator_KDJ_K[count_interval].LineWidth = 1 + count_interval;
                    Indicator_KDJ_D[count_interval].LineWidth = 1 + count_interval;
                    Indicator_KDJ_J[count_interval].LineWidth = 1 + count_interval;

                    Indicator_KDJ_K[count_interval].IsVisible = false;
                    Indicator_KDJ_D[count_interval].IsVisible = false;
                    Indicator_KDJ_J[count_interval].IsVisible = false;
                }
                //------------------------------------------------------------------------
                d_indicator_OBV = new double[intervals.Count(), 4][];
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + "_OBV \r\n" +
                        "order by time ";
                    var klineIndicatorData = Operations.KlineData.GetIndicatorData_OBV(sqlQuery);
                    d_indicator_OBV[count_interval, 0] = new double[klineIndicatorData.Count];
                    d_indicator_OBV[count_interval, 1] = new double[klineIndicatorData.Count];
                    d_indicator_OBV[count_interval, 2] = new double[klineIndicatorData.Count];
                    d_indicator_OBV[count_interval, 3] = new double[klineIndicatorData.Count];
                    //d_indicator_OBV[ count_interval, count_interval][5] = 0;

                    for (int counter_klineIndicatorData = 0; counter_klineIndicatorData < klineIndicatorData.Count; counter_klineIndicatorData++)
                    {
                        d_indicator_OBV[count_interval, 0][counter_klineIndicatorData] = Indicators.Auxiliary.BTS_DateTime(klineIndicatorData[counter_klineIndicatorData].TIME).ToOADate();
                        d_indicator_OBV[count_interval, 1][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].OBV_6;
                        d_indicator_OBV[count_interval, 2][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].OBV_12;
                        d_indicator_OBV[count_interval, 3][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].OBV_24;
                    }
                    Indicator_OBV_6[count_interval] = FP_OBV.Plot.AddSignalXY(d_indicator_OBV[count_interval, 0], d_indicator_OBV[count_interval, 1], color: Color.Yellow);
                    Indicator_OBV_12[count_interval] = FP_OBV.Plot.AddSignalXY(d_indicator_OBV[count_interval, 0], d_indicator_OBV[count_interval, 2], color: Color.Red);
                    Indicator_OBV_24[count_interval] = FP_OBV.Plot.AddSignalXY(d_indicator_OBV[count_interval, 0], d_indicator_OBV[count_interval, 3], color: Color.Purple);

                    Indicator_OBV_6[count_interval].LineWidth = 1 + count_interval;
                    Indicator_OBV_12[count_interval].LineWidth = 1 + count_interval;
                    Indicator_OBV_24[count_interval].LineWidth = 1 + count_interval;

                    Indicator_OBV_6[count_interval].IsVisible = false;
                    Indicator_OBV_12[count_interval].IsVisible = false;
                    Indicator_OBV_24[count_interval].IsVisible = false;
                }
                #endregion

                DataTable MyTable = new DataTable(); // 1
                MyTable.Columns.Add("interval_position", typeof(int));
                MyTable.Columns.Add("RSI_limit", typeof(int));
                MyTable.Columns.Add("getProfit", typeof(double));
                MyTable.Columns.Add("stopLoss", typeof(double));
                MyTable.Columns.Add("stepSize", typeof(int));
                MyTable.Columns.Add("pnl", typeof(double));
                MyTable.Columns.Add("SuccessCount", typeof(int));
                MyTable.Columns.Add("FailCount", typeof(int));
                MyTable.Columns.Add("NeutralCount", typeof(int));
                interval_position = 4;//for (int interval_position = 9; interval_position > 0; interval_position--)
                {
                    int RSI_limit = 30;//for (int RSI_limit = 20; RSI_limit < 35; RSI_limit++)
                    {
                        List<double> HotSpots_X = new();
                        List<double> HotSpots_Y = new();
                        List<double> HotSpots_X_end = new();
                        List<double> HotSpots_Y_end = new();
                        List<string> HotSpots_end_status = new();
                        for (int i = 50; i < d_indicator_RSI[interval_position, 0].Count(); i++)
                        {
                            if (
                                d_indicator_RSI[interval_position, 1][i - 1] < RSI_limit &&
                                d_indicator_RSI[interval_position, 1][i] > RSI_limit &&

                                d_indicator_KDJ[interval_position, 2][i] > d_indicator_KDJ[interval_position, 1][i] &&
                                d_indicator_KDJ[interval_position, 1][i] > d_indicator_KDJ[interval_position, 3][i]
                                )
                            {
                                int t1 = Array.IndexOf(d_coinPrice[0, 0], d_coinPrice[interval_position, 0][i]);
                                HotSpots_X.Add(d_coinPrice[0, 0][t1]);
                                HotSpots_Y.Add(d_coinPrice[0, 2][t1]);
                                if (noGraph == false)
                                {
                                    DrawVerticalLine_All(d_coinPrice[0, 0][t1]);
                                }
                            }
                        }
                        for (double getProfit = 1.002; getProfit < 1.02; getProfit += 0.001)
                        {
                            double stopLoss = 0.90;//for (double stopLoss = 0.98; stopLoss > 0.80; stopLoss -= 0.01)
                            {
                                for (int stepSize = 60; stepSize < (600); stepSize = stepSize + 10)
                                //stepSize = 60 * 60;
                                {


                                    int SuccessCount = 0;
                                    int FailCount = 0;
                                    int NeutralCount = 0;
                                    double pnl = 0;
                                    for (int i = 0; i < HotSpots_X.Count; i++)
                                    {
                                        int t1 = Array.IndexOf(d_coinPrice[0, 0], HotSpots_X[i]);
                                        for (int j = t1; j < d_coinPrice[0, 0].Count(); j++)
                                        {
                                            if (HotSpots_Y[i] * stopLoss >= d_coinPrice[0, 2][j])
                                            {
                                                if (noGraph == false)
                                                {
                                                    HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
                                                    HotSpots_Y_end.Add(d_coinPrice[0, 2][j]);
                                                }
                                                //pnl -= ((1.0d - stopLoss) * 100.0d) + 1.0d;
                                                double pnl_Diff = (((d_coinPrice[0, 2][j] - d_coinPrice[0, 2][t1]) / d_coinPrice[0, 2][t1]) * 100) - 1;
                                                pnl += pnl_Diff;
                                                HotSpots_end_status.Add("Loss");
                                                FailCount++;
                                                break;
                                            }
                                            else if (HotSpots_Y[i] * getProfit <= d_coinPrice[0, 1][j])
                                            {
                                                if (noGraph == false)
                                                {
                                                    HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
                                                    HotSpots_Y_end.Add(d_coinPrice[0, 1][j]);
                                                }
                                                //pnl += ((getProfit - 1.0d) * 100.0d) - 1.0d;
                                                double pnl_Diff = (((d_coinPrice[0, 2][j] - d_coinPrice[0, 2][t1]) / d_coinPrice[0, 2][t1]) * 100) - 1;
                                                pnl += pnl_Diff;
                                                HotSpots_end_status.Add("Profit");
                                                SuccessCount++;
                                                break;
                                            }
                                            else if (j - t1 > stepSize)
                                            {
                                                if (noGraph == false)
                                                {
                                                    HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
                                                    HotSpots_Y_end.Add(d_coinPrice[0, 2][j]);
                                                }
                                                double a1 = (d_coinPrice[0, 2][t1]);
                                                double a2 = (d_coinPrice[0, 2][j]);
                                                double pnl_Diff = (((d_coinPrice[0, 2][j] - d_coinPrice[0, 2][t1]) / d_coinPrice[0, 2][t1]) * 100) - 1;
                                                pnl += pnl_Diff;
                                                if (pnl_Diff >= 0.0)
                                                {
                                                    HotSpots_end_status.Add("Neutral+");
                                                }
                                                else// (pnl_Diff < 0.0)
                                                {
                                                    HotSpots_end_status.Add("Neutral-");
                                                }
                                                NeutralCount++;
                                                break;
                                            }
                                        }
                                    }
                                    if (noGraph == false)
                                    {
                                        for (int i = 0; i < HotSpots_end_status.Count; i++)
                                        {
                                            var rp = FP_CoinPrice.Plot.AddRectangle(
                                                xMin: HotSpots_X[i], xMax: HotSpots_X_end[i], yMin: HotSpots_Y[i], yMax: HotSpots_Y_end[i]);
                                            if (HotSpots_end_status[i] == "Profit")
                                            {
                                                rp.Color = Color.FromArgb(100, Color.Green);
                                            }
                                            else if (HotSpots_end_status[i] == "Loss")
                                            {
                                                rp.Color = Color.FromArgb(100, Color.Red);
                                            }
                                            else if (HotSpots_end_status[i] == "Neutral+")
                                            {
                                                rp.Color = Color.FromArgb(100, Color.LightGreen);
                                            }
                                            else if (HotSpots_end_status[i] == "Neutral-")
                                            {
                                                rp.Color = Color.FromArgb(100, Color.IndianRed);
                                            }

                                        }
                                    }
                                    MyTable.Rows.Add(interval_position, RSI_limit, getProfit, stopLoss, stepSize, pnl, SuccessCount, FailCount, NeutralCount);
                                }
                            }
                        }
                    }
                    //Save(MyTable, interval_position.ToString());
                    Save2(MyTable, interval_position.ToString());
                }

                //int asd = 0;
            }
            ));
        }
        void Save(DataTable dt, string name)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(name + "test.csv", sb.ToString());
            //dt.Clear();
        }
        void Save2(DataTable dt, string name)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(";", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(";", fields));
            }

            File.WriteAllText(name + "test2.csv", sb.ToString());
            dt.Clear();
        }


        //void CreateSignalPoints(int RSI_limit)
        //{
        //    for (int i = 50; i < d_indicator_RSI[4, 0].Count(); i++)
        //    {
        //        if (
        //            d_indicator_RSI[4, 1][i - 1] < RSI_limit &&
        //            d_indicator_RSI[4, 1][i] > RSI_limit &&

        //            d_indicator_KDJ[4, 2][i] > d_indicator_KDJ[4, 1][i] &&
        //            d_indicator_KDJ[4, 1][i] > d_indicator_KDJ[4, 3][i]
        //            )
        //        {
        //            int t1 = Array.IndexOf(d_coinPrice[0, 0], d_coinPrice[4, 0][i]);
        //            HotSpots_X.Add(d_coinPrice[0, 0][t1]);

        //            DrawVerticalLine_All(d_coinPrice[0, 0][t1]);
        //            HotSpots_Y.Add(d_coinPrice[0, 2][t1]);
        //        }
        //    }
        //}
        //void UseSignalPoints(double getProfit, double stopLoss)
        //{
        //    double pnl = 0;
        //    for (int i = 0; i < HotSpots_X.Count; i++)
        //    {
        //        int t1 = Array.IndexOf(d_coinPrice[0, 0], HotSpots_X[i]);
        //        for (int j = t1; j < d_coinPrice[0, 0].Count(); j++)
        //        {
        //            if (HotSpots_Y[i] * stopLoss >= d_coinPrice[0, 2][j])
        //            {
        //                HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
        //                HotSpots_Y_end.Add(d_coinPrice[0, 2][j]);
        //                pnl -= 1 - stopLoss;
        //                HotSpots_end_status.Add("Loss");
        //                FailCount++;
        //                break;
        //            }
        //            else if (HotSpots_Y[i] * getProfit <= d_coinPrice[0, 1][j])
        //            {
        //                HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
        //                HotSpots_Y_end.Add(d_coinPrice[0, 1][j]);
        //                pnl += getProfit - 1;
        //                HotSpots_end_status.Add("Profit");
        //                SuccessCount++;
        //                break;
        //            }
        //        }
        //    }
        //    for (int i = 0; i < HotSpots_end_status.Count; i++)
        //    {
        //        var rp = FP_CoinPrice.Plot.AddRectangle(
        //            xMin: HotSpots_X[i], xMax: HotSpots_X_end[i], yMin: HotSpots_Y[i], yMax: HotSpots_Y_end[i]);
        //        if (HotSpots_end_status[i] == "Profit")
        //        {
        //            rp.Color = Color.FromArgb(100, Color.Green);
        //        }
        //        else
        //        {
        //            rp.Color = Color.FromArgb(100, Color.Red);
        //        }

        //    }
        //}


        private void FP_CoinPrice_AxesChanged(object? sender, EventArgs e)
        {
            foreach (var fp in FormsPlots)
            {
                if (fp == FP_CoinPrice)
                    continue;
                if (fp == FP_RSI)
                {
                    var newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits();
                    var x_newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits(0, 1);
                    x_newAxisLimits = x_newAxisLimits.WithY(0, 100);
                    // disable events briefly to avoid an infinite loop
                    fp.Configuration.AxesChangedEventEnabled = false;
                    fp.Plot.SetAxisLimits(x_newAxisLimits);
                    fp.Render();
                    fp.Configuration.AxesChangedEventEnabled = true;
                }
                if (fp == FP_KDJ)
                {
                    var newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits();
                    var x_newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits(0, 1);
                    x_newAxisLimits = x_newAxisLimits.WithY(0, 100);
                    // disable events briefly to avoid an infinite loop
                    fp.Configuration.AxesChangedEventEnabled = false;
                    fp.Plot.SetAxisLimits(x_newAxisLimits);
                    fp.Render();
                    fp.Configuration.AxesChangedEventEnabled = true;
                }
                if (fp == FP_OBV)
                {
                    var newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits();
                    var x_newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits(0, 1);

                    //x_newAxisLimits = x_newAxisLimits.WithY(-1000000, 1000000);
                    // disable events briefly to avoid an infinite loop
                    fp.Configuration.AxesChangedEventEnabled = false;
                    //fp.Plot.SetAxisLimits(x_newAxisLimits);
                    //fp.Plot.SetAxisLimitsX(x_newAxisLimits);
                    fp.Plot.SetAxisLimitsX(x_newAxisLimits.XMin, x_newAxisLimits.XMax);
                    fp.Render();
                    fp.Configuration.AxesChangedEventEnabled = true;
                }
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
