using System;
using System.Threading.Tasks;

namespace TakeHomeAssignment_jc
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var tests = new Tests();
            await tests.RunTests();

            Console.WriteLine("Tests completed!");
        }
    }
}
