// Name: Brandon Tran
// CECS 475: Phuong Nguyen
// Date: 1/29/2020
// Lab Assignment 3: Delegates and Events Part C and D

using System;

namespace CECS475Lab3_1.PartC_D
{
    class Program
    {
        static void Main(string[] args)
        {
            // Delegate event without passing data using .NET EventHandler
            Number myNumber = new Number(100000);
            myNumber.PrintMoney();
            myNumber.PrintNumber();

            // Delegate event with passing data using .NET EventHandler
            NumberPass myNumberPass = new NumberPass(100000);
            myNumberPass.PrintMoneyPass();
            myNumberPass.PrintNumberPass();
        }
    }
    // The class Number offers the event NewNumIsHere of type EventHandler<NumInfoEventsArgsPass>. Events typically use methods with two parameters
    // explaining that the first parameter is an object and contains the sender of the event, and the second parameter provides information about the
    // event. Generic delegate EventHandler<NewNumIsHere> defines a handler that returns void and accepts two parameters
    // The first parameter needs to be of type object, and the second parameter is of EventArgs. It must derive from the base class EventArgs,
    // which is the case with NumInfoEventsArgsPass. The compiler creates a variable of the delegate type EventHandler<NumInfoEventsArgsPass> and
    // adds methods to subscribe and unsubscribe from the delegate. 
    class Number
    {
        private PrintHelper _printHelper;
        public Number(int val)
        {
            _value = val;

            _printHelper = new PrintHelper();
            // Subscribe to beforePrintEvent event
            _printHelper.NewNumEvent += NewNum;
        }

        static void NewNum(object sender, EventArgs e)
        {
            Console.WriteLine("BeforPrintEventHandler: PrintHelper is going to print a value", EventArgs.Empty);
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void PrintMoney()
        {
            _printHelper.PrintMoney(_value);
        }

        public void PrintNumber()
        {
            _printHelper.PrintNumber(_value);
        }
    }

    
    public class PrintHelper
    {
        public event EventHandler NewNumEvent;
        public delegate void BeforePrint();

        public void PrintNumber(int num)
        {
            // Call delegate method before going to print
            NewNumEvent?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public void PrintMoney(int money)
        {
            NewNumEvent?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("Money: {0:C}", money);
        }

    }

    public class NumInfoEventArgsPass : EventArgs
    {
        public NumInfoEventArgsPass(string number)
        {
            NumberPass = number;
        }

        public string NumberPass { get; }
    }

    // The class PrintHelperPass fires the event by calling the Invoke method of the delegate.
    // This invokes all the handlers that are subscribed to the event. For the class NumberPass,
    // we can briefly explained that we had to change the subscription implement to the event by calling
    // to the method of NewNumIsHerePass.
    class NumberPass
    {
        private PrintHelperPass _printHelperPass;
        public NumberPass(int val)
        {
            _valuePass = val;

            _printHelperPass = new PrintHelperPass();
            // Subscribe to beforePrintEvent event
            _printHelperPass.NewNumInfoPass += NewNumIsHerePass;
        }
        public void NewNumIsHerePass(object sender, NumInfoEventArgsPass e)
        {
            Console.WriteLine("BeforePrintEvent fires from {0}", e.NumberPass);
        }

        private int _valuePass;
        public int ValuePass
        {
            get { return _valuePass; }
            set { _valuePass = value; }
        }
        public void PrintMoneyPass()
        {
            _printHelperPass.PrintMoneyPass(_valuePass);
        }
        public void PrintNumberPass()
        {
            _printHelperPass.PrintNumberPass(_valuePass);
        }
    }

    public class PrintHelperPass
    {
        public event EventHandler<NumInfoEventArgsPass> NewNumInfoPass;
        public delegate void BeforePrintPass(string message);

        public void PrintNumberPass(int num)
        {
            NewNumInfoPass?.Invoke(this, new NumInfoEventArgsPass("PrintNumber"));
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public void PrintMoneyPass(int money)
        {
            NewNumInfoPass?.Invoke(this, new NumInfoEventArgsPass("PrintMoney"));
            Console.WriteLine("Money: {0:C}", money);
        }

    }

}