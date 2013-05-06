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
            var methodInfo = typeof(TestClass2).GetMethod("Start");
            var methodIL = methodInfo.GetInstructions();

            foreach (var instruction in methodIL.Instructions)
                Console.WriteLine(instruction);

            Console.WriteLine("Press any key to execute");
            Console.ReadLine();

            //execute the instructions inside the virtual CLR.
            Console.WriteLine("--Interpreter-- (compile in Release mode for best performance.)");
            var v_clr = new VirtualCLR();
            v_clr.ExecuteILMethod(methodIL);
        }
    }

    public class TestClass2
    {
        public void Start()
        {
            var cEntries = Directory.GetFileSystemEntries(@"C:\");

            var matches = cEntries.Where(w => w.ToUpper().Contains("SQL"));

            foreach (var match in matches)
                Console.WriteLine(match);

            return;
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

    public class TestClass
    {
        public void TestMethodA()
        {
            Console.WriteLine("TestMethodA");

            Console.Write("A:");
            int a = int.Parse(Console.ReadLine());

            Console.Write("B:");
            int b = int.Parse(Console.ReadLine());

            Add(a, b);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public void Add(int a, int b)
        {
            Console.WriteLine("Adding A to B");

            int c = a + b;

            Console.WriteLine("Result: " + c);
        }
    }
}
