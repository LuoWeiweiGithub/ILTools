using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Animaonline.ILTools;
using Animaonline.ILTools.vCLR;
using System.Collections.Generic;

namespace ILTools.Tests
{
    static class Program
    {
        /*
         * Entry Point
         */
        static void Main(string[] args)
        {
            Console.Title = "vCLR - (compile in Release mode for best performance.)";

            var methodInfo = typeof(FieldsTest).GetMethod("Main", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var methodIL = methodInfo.GetInstructions();

            Console.WriteLine("Press any key to execute");
            Console.ReadLine();

            //execute the instructions inside the virtual CLR.
            var v_clr = new VirtualCLR(vCLRScope.Class);
            v_clr.ExecuteILMethod(methodIL);
        }
    }

    public class FieldsTest
    {
        void Main()
        { 
            var fieldValue = Console.ReadLine();

            setField(fieldValue);

            printField();
        } 
        public object field;

        public void setField(object value)
        {
            field = value;
        }

        public void printField()
        {
            Console.WriteLine(field);
        }
    }

    public class TestClass
    {
        public void Benchmark()
        {
            var stp = new Stopwatch();
            stp.Start();

            int result = 0;

            for (int idx = 0; idx < 10000000; idx++)
            {
                result += 1;
            }

            stp.Stop();

            Console.WriteLine("Execution completed, time elapsed: " + stp.Elapsed);
        }
    }
}
