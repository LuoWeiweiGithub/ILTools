using System;
using System.Windows.Forms;

namespace ILTools.Tests
{
    static class Program
    {
        /*
         * Entry Point
         */
        static void Main(string[] args)
        {

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
