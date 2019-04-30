using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CallStackTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var classA = new ClassA();
            classA.MethodA();
            Console.ReadLine();
        }
    }

    public class ClassA
    {
        private readonly ClassB _classB;

        public ClassA()
        {
            this._classB = new ClassB();
        }

        public void MethodA()
        {
            Console.WriteLine($"Call MethodA, Call Stack:{MethodBase.GetCurrentMethod().GetFullCallStack()}");
            this._classB.MethodB();
        }
    }

    public class ClassB
    {
        private readonly ClassC _classC;

        public ClassB()
        {
            this._classC = new ClassC();
        }

        public void MethodB()
        {
            Console.WriteLine($"Call MethodB, Call Stack:{MethodBase.GetCurrentMethod().GetFullCallStack()}");
            this._classC.MethodC();
        }
    }

    public class ClassC
    {
        public void MethodC()
        {
            Console.WriteLine($"Call MethodC, Call Stack:{MethodBase.GetCurrentMethod().GetFullCallStack()}");
            ClassD.MethodD();
        }
    }

    public class ClassD
    {
        public static void MethodD()
        {
            Console.WriteLine($"Call MethodD, Call Stack:{MethodBase.GetCurrentMethod().GetFullCallStack()}");
        }
    }

    public static class MethodExtension
    {
        public static string GetFullCallStack(this MethodBase methodBase)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("StackTrace ToString Information:" + stackTrace.ToString());
            var stackTraces = stackTrace.GetFrames();
            return string.Join(" <-- ", stackTraces.Select(a => a.GetMethod().Name));
        }
    }
}
