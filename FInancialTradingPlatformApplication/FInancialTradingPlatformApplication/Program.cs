using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;


//https://chat.openai.com/share/bca103ad-59fd-40a2-be63-b40e79524ecd

namespace FInancialTradingPlatformApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Method name : Main, ThreadID {Thread.CurrentThread.ManagedThreadId}"); // manages current thread IDs
             StockMarketTechnicalAnalysis stockMarketTechnicalAnalysisData = new StockMarketTechnicalAnalysis("STZK", new DateTime(2010,1,1), new DateTime(2020,1,1));
            //constructor calling

            DateTime dateTimeBefore = DateTime.Now;


            //THESE ARE SYNCHRONOUS CALL METHODS

            //decimal[] data1 = stockMarketTechnicalAnalysisData.GetOpeningPrices();
            //decimal[] data2 = stockMarketTechnicalAnalysisData.GetClosingPrices();
            //decimal[] data3 = stockMarketTechnicalAnalysisData.GetPriceHighs();
            //decimal[] data4 = stockMarketTechnicalAnalysisData.GetPriceLows();
            //decimal[] data5 = stockMarketTechnicalAnalysisData.CalculateStockastics();
            //decimal[] data6 = stockMarketTechnicalAnalysisData.CalculateFastMovingAverage();
            //decimal[] data7 = stockMarketTechnicalAnalysisData.CalculateSlowMovingAverage();
            //decimal[] data8 = stockMarketTechnicalAnalysisData.CalculateUpperBoundBollingerBand();
            //decimal[] data9 = stockMarketTechnicalAnalysisData.CalculateLowerBoundBollingerBand();

            DateTime dateTimeAfter = DateTime.Now;
            TimeSpan timeSpan = dateTimeAfter.Subtract(dateTimeBefore);

            //THESE AREcASYNCHRONOUS METHODS
            List<Task<decimal[]>> tasks = new List<Task<decimal[]>>();

            Task<decimal[]> getOpeningPricesTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetOpeningPrices());
            Task<decimal[]> getClosingPricesTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetClosingPrices());
            Task<decimal[]> getPricesHighsTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetPriceHighs());
            Task<decimal[]> getPricesLowsTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetPriceLows());
            Task<decimal[]> calculateStockasticsTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateStockastics());
            Task<decimal[]> calculateFastMovingAverageTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateFastMovingAverage());
            Task<decimal[]> calculateSlowMovingAverageTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateSlowMovingAverage());
            Task<decimal[]> calculateUpperBollingerBandTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateUpperBoundBollingerBand());
            Task<decimal[]> calculateLowerBollingerBandTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateLowerBoundBollingerBand());
            //Task provides a method to run Tasks (Task.Run()) and a method for waiting for tasks to complete (Task.Wait())
            //Remeber Task is not a keyword
            // So basically to write the code as an async method u have to use Task.Run() and give it a function call
            // followed by a =>
            // U can write Task normally or for a data type Task<T> T is data type


            tasks.Add(getOpeningPricesTask);
            tasks.Add(getClosingPricesTask);    
            tasks.Add(getPricesHighsTask);  
            tasks.Add(getPricesLowsTask);
            tasks.Add(calculateStockasticsTask);
            tasks.Add(calculateFastMovingAverageTask);
            tasks.Add(calculateSlowMovingAverageTask);
            tasks.Add(calculateUpperBollingerBandTask);
            tasks.Add(calculateLowerBollingerBandTask);


            Task.WaitAll(tasks.ToArray());

            decimal[] data1 = tasks[0].Result;
            decimal[] data2 = tasks[1].Result;
            decimal[] data3 = tasks[2].Result;
            decimal[] data4 = tasks[3].Result;
            decimal[] data6 = tasks[5].Result;
            decimal[] data7 = tasks[6].Result;
            decimal[] data8 = tasks[7].Result;
            decimal[] data9 = tasks[8].Result;

            Console.WriteLine($"Total time taken for the operations to complete {timeSpan.Seconds} {(timeSpan.Seconds>1 || timeSpan.Seconds < 1 ? "seconds" : "second")}");

            //DisplayDataOnChart(data1,data2, data3, data4);

            Console.ReadKey();
        }
    public static void DisplayDataOnChart(decimal[] data1, decimal[] data2, decimal[] data3, decimal[] data4)
        {
            Console.WriteLine("Data is displayed on the chart");
        }
    }

    public class StockMarketTechnicalAnalysis
    {
        public StockMarketTechnicalAnalysis(string StockSymbol, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            //Code here gets data from remote server
        }

        public decimal[] GetOpeningPrices() // Gets opening prices
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(GetOpeningPrices)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep( 1000 );

            data = new decimal[] { };

            return data;
        }

        public decimal[] GetClosingPrices() // gets closing prices
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(GetClosingPrices)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] GetPriceHighs() // gets price highs
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(GetPriceHighs)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] GetPriceLows() // gets price lows
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(GetPriceLows)}, ThreadId: {Thread.CurrentThread.ManagedThreadId} ");
            Thread.Sleep(1000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] CalculateStockastics() // calculates stockastics
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(CalculateStockastics)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] CalculateFastMovingAverage() // calcs fast moving averages
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(CalculateFastMovingAverage)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(6000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] CalculateSlowMovingAverage() // calclatrs slow moving averrages
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(CalculateSlowMovingAverage)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(7000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] CalculateUpperBoundBollingerBand() // calculates upper bollinger bound
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(CalculateUpperBoundBollingerBand)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);

            data = new decimal[] { };

            return data;
        }

        public decimal[] CalculateLowerBoundBollingerBand() // calculates lower bollinger bound
        {
            decimal[] data;
            Console.WriteLine($"Method name : {nameof(CalculateLowerBoundBollingerBand)}, ThreadID {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);

            data = new decimal[] { };

            return data;
        }


    }
}