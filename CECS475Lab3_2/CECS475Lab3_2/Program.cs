// Name: Brandon Tran
// CECS 475: Phuong Nguyen
// Date: 2/13/2020
// Lab Assignment 3: Stocks

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CECS475Lab3_2
{
    class Program
    {

        public class StockNotification : EventArgs
        {

            public StockNotification(string sName, int cValue, int nChanges, int iValue)
            {
                StockName = sName;
                CurrentValue = cValue;
                NumberOfChanges = nChanges;
                InitialValue = iValue;

            }

            public string StockName { get; set; }
            public int CurrentValue { get; set; }
            public int NumberOfChanges { get; set; }
            public int InitialValue { get; set; }

        }

        /// <summary>
        /// Create the class Stock with the following attributes:
        ///     - Stock name
        ///     - Stock Initial Value
        ///     - Maximum change (the range within a stock can change every time unit)
        ///     - Notification threshold (the threshold above or below which the collection of brokers who control the stock must be notified)
        ///
        /// You are required to implement other members in the class Stock that are needed. When a stock object is created, a thread is started.
        /// This thread causes the stock's value to be modified every 500 milliseconds. If its value changes from its initial value by more than the
        /// specified notification threshold, an event method is invoked. This invokes the stockEvent (of event-type StockNotification) and multicasts
        /// a notification to all listeners who have registered with stockEvent.
        /// </summary>
        public class Stock
        {
            public event EventHandler<StockNotification> StockEvent;

            // Class Stock Attributes
            private string _stockName;
            private int _initialValue;
            private int _maxChange;
            private int _threshold;

            public int _currentValueOfStock;
            public int _numOfChanges = 0;

            /// <summary>
            /// Create a default constructor. When the stock object is created, a thread is started. Showcase the use of Thread(ThreadStart) constructor with Non-Static method
            /// </summary>
            /// <param name="stockName"></param>
            /// <param name="initialValue"></param>
            /// <param name="maxChange"></param>
            /// <param name="threshold"></param>
            public Stock(string stockName, int initialValue, int maxChange, int threshold)
            {
                _stockName = stockName;
                _initialValue = initialValue;
                _currentValueOfStock = _initialValue;
                _maxChange = maxChange;
                _threshold = threshold;

                // Creating and initializing a thread with Thread(ThreadStart) constructor
                // Thread childref = new ThreadStart(CallToChildThread);
                Thread childref = new Thread(new ThreadStart(CallToChildThread));
                childref.Start();
            }

            /// <summary>
            /// Once a thread is initialized and created within the constructor to start, it will call to the child thread.
            /// Every 500 milliseconds, the Stock(thread) will change. As well as invoking a ChangeStockValue method that will change the price value of the stock
            /// </summary>
            public void CallToChildThread()
            {
                for (int i = 0; i < 100; i++)
                {
                    // This thread causes the stock's value to be modified every 500 milliseconds. 
                    Thread.Sleep(500);

                    // If its value changes from its initial value by more than the specified notification theshold, an event method is invoked.
                    ChangeStockValue();

                }
            }

            public void ChangeStockValue()
            {
                // When the ChangeStockValue method is called upon, increase the number of changes towards the stock.
                _numOfChanges++;

                // Assume that you don't have to ask the user what value to change it to and apply a Random Number Generator
                Random RNG = new Random();
                int randomValue = RNG.Next(1, _maxChange);
                _currentValueOfStock += randomValue;
           

                if ((_currentValueOfStock - _initialValue) > _threshold)
                {
                    StockNotification args = new StockNotification(_stockName, _currentValueOfStock, _numOfChanges, _initialValue);
                    OnStockEvent(args);
                }

            }

            public void OnStockEvent(StockNotification e)
            {
                StockEvent?.Invoke(this, e);
            }

        }


        /// <summary>
        /// Create the class StockBroker which has fields broker name and stocks, a List of Stock. This latter field is not used in this application
        /// but could be used to obtain the stocks currently controlled by a given broker. The addStock method registers the Notify listerner with the
        /// stock (in addition to adding it to the list of stocks held by the broker). This Notify method outputs to the console the name, value, and the
        /// number of changes of the stock whose value is out of the range given the stock's notification threshold.
        /// </summary>
        /// <summary>
        /// Class StockBroker which represents a stock broker.
        /// </summary>
        public class StockBroker
        {
            private string brokerName; // name of the broker 
            List<Stock> stocks; //list to hold the stocks being added to a broker
            static object ToLock = new object(); //lock object variable to handle synchronization

            /// <summary>
            /// Initialized constructor
            /// </summary>
            /// <param name="BrokerName"></param>
            public StockBroker(string BrokerName)
            {
                brokerName = BrokerName;
                stocks = new List<Stock>();
            }

            ///
            /// 
            /// <summary>
            /// Add a stock to the list of stocks. Then, it will subscribe to the broker
            /// so that it can be notified to price changes.
            /// </summary>
            /// <param name="s"></param>
            public void AddStock(Stock s)
            {
                s.StockEvent += Notification; //subcribing to stockEvent
                stocks.Add(s); //adding stock to list stocks
            }

            /// <summary>
            /// Event handler method
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void Notification(object sender, StockNotification e)
            {
                lock (ToLock)
                {
                    Console.WriteLine(brokerName.PadRight(12) + e.StockName.PadRight(12) + e.CurrentValue.ToString().PadRight(12) + e.NumberOfChanges.ToString().PadRight(12));
                }
                lock (ToLock)
                {

                    string directory = "/Users/brandontran/Projects/CECS475Lab3_2/CECS475Lab3_2";
                    string filename = "stocks.txt";
                    string path = directory + filename;

                    bool FileExists = File.Exists(path);

                    if (!File.Exists(path))
                    {
                        using (StreamWriter writer = File.CreateText(path))
                        {
                            writer.WriteLine("Broker".PadRight(12) + "Stock".PadRight(12) + "Value".PadRight(12) + "Changes".PadRight(12));
                            writer.WriteLine(e.StockName.PadRight(12) + e.CurrentValue.ToString().PadRight(12) + e.NumberOfChanges.ToString().PadRight(12) + e.InitialValue.ToString().PadRight(12));
                            writer.Flush();
                        }
                    }
                    else if (File.Exists(path))
                    {
                        using (StreamWriter writer = File.AppendText(path))
                        {
                            writer.WriteLine(brokerName.PadRight(12) + e.StockName.PadRight(12) + e.CurrentValue.ToString().PadRight(12) + e.NumberOfChanges.ToString().PadRight(12));
                            writer.Flush();
                        }
                    }



                }
            }//end of Notify method
        }//end of StockBroker class

        /// <summary>
        /// The listing below presents a main driver class, StockApplication
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Broker".PadRight(12) + "Stock".PadRight(12) + "Value".PadRight(12) + "Changes".PadRight(12));
            Console.WriteLine();
            Stock stock1 = new Stock("Technology", 160, 5, 15);
            Stock stock2 = new Stock("Retail", 30, 2, 6);
            Stock stock3 = new Stock("Banking", 90, 4, 10);
            Stock stock4 = new Stock("Commodity", 500, 20, 50);

            StockBroker b1 = new StockBroker("Broker 1");
            b1.AddStock(stock1);
            b1.AddStock(stock2);

            StockBroker b2 = new StockBroker("Broker 2");
            b2.AddStock(stock1);
            b2.AddStock(stock3);
            b2.AddStock(stock4);

            StockBroker b3 = new StockBroker("Broker 3");
            b3.AddStock(stock1);
            b3.AddStock(stock3);

            StockBroker b4 = new StockBroker("Broker 4");
            b4.AddStock(stock1);
            b4.AddStock(stock2);
            b4.AddStock(stock3);
            b4.AddStock(stock4);
        }
    }
}
