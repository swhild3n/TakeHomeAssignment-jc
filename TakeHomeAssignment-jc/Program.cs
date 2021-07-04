using System;

namespace TakeHomeAssignment_jc
{
    class Program
    {
        static void Main(string[] args)
        {
            var actionAdder = new ActionAdder();
            var error = "";

            foreach(var arg in args)
            {
                error = actionAdder.addAction(arg);
                if(error != "")
                {
                    Console.WriteLine(error);
                }
            }
            var stats = actionAdder.getStats();

            Console.WriteLine("Statistics: \n" + stats);

        }
    }
}
