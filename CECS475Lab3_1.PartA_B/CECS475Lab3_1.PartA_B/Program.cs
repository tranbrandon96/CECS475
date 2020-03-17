// Name: Brandon Tran
// CECS 475: Phuong Nguyen
// Date: 2/6/2020
// Lab Assignment 3: Delegates and Events Part A and B 

using System;

namespace CECS475Lab3_1.PartA_B
{
    class Program
    {
        static void Main(string[] args)
        {
            // Delegate event without passing data output
            Number myNumber = new Number(100000);
            myNumber.PrintMoney();
            myNumber.PrintNumber();

            // Delegate event with passing data output
            NumberPass myNumberPass = new NumberPass(100000);
            myNumberPass.PrintMoneyPass();
            myNumberPass.PrintNumberPass();
        }
    }
}

class Number
{
    private PrintHelper _printHelper;

    public Number(int val)
    {
        _value = val;

        _printHelper = new PrintHelper();
        // Subscribe to beforePrintEvent event
        _printHelper.beforePrintEvent += printHelper_beforePrintEvent;
    }
    // BeforePrintevent handler
    void printHelper_beforePrintEvent()
    {
        Console.WriteLine("BeforPrintEventHandler: PrintHelper is going to print a value");
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
    // Declare delegate 
    public delegate void BeforePrint();

    // Declare event of type delegate
    public event BeforePrint beforePrintEvent;

    public PrintHelper()
    {

    }

    public void PrintNumber(int num)
    {
        // Call delegate method before going to print
        if (beforePrintEvent != null)
            beforePrintEvent();

        Console.WriteLine("Number: {0,-12:N0}", num);
    }

    public void PrintMoney(int money)
    {
        if (beforePrintEvent != null)
            beforePrintEvent();

        Console.WriteLine("Money: {0:C}", money);
    }

}


// Delegate event with passing data
public class PrintHelperPass
{
    public delegate void BeforePrintPass(string message);
    public event BeforePrintPass beforePrintEventPass;
    public void PrintNumberPass(int num)
    {
        if (beforePrintEventPass != null)
            beforePrintEventPass("PrintNumber");
        Console.WriteLine("Number: {0,-12:N0}", num);
    }

    public void PrintMoneyPass(int money)
    {
        if (beforePrintEventPass != null)
            beforePrintEventPass("PrintMoney");
        Console.WriteLine("Money: {0:C}", money);
    }

}

class NumberPass
{
    private PrintHelperPass _printHelperPass;
    public NumberPass(int val)
    {
        _valuePass = val;

        _printHelperPass = new PrintHelperPass();
        // Subscribe to beforePrintEvent event
        _printHelperPass.beforePrintEventPass += printHelperPass_beforePrintEventPass;
    }
    // BeforePrintevent handler
    void printHelperPass_beforePrintEventPass(string message)
    {
        Console.WriteLine("BeforePrintEvent fires from {0}", message);
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
