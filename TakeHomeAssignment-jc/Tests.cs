using System;
using System.Threading.Tasks;

namespace TakeHomeAssignment_jc
{
    class Tests
    {
        public async Task RunTests()
        {
            var tester = new ActionTester();

            //Test adding inputs that are given in the assignment
            tester.DoTest("Add Given Inputs", x =>
            {
                var t1 = x.addAction("{\"action\":\"jump\",\"time\":100}");
                var t2 = x.addAction("{\"action\":\"run\", \"time\":75}");
                var t3 = x.addAction("{\"action\":\"jump\", \"time\":200}");
                return true == string.IsNullOrEmpty(t1)
                    && true == string.IsNullOrEmpty(t2)
                    && true == string.IsNullOrEmpty(t3);
            });

            //Test getting correct stats from inputs given in the assignment
            tester.DoTest("Get Stats From Given Inputs", x =>
            {
                var t1 = x.addAction("{\"action\":\"jump\",\"time\":100}");
                var t2 = x.addAction("{\"action\":\"run\", \"time\":75}");
                var t3 = x.addAction("{\"action\":\"jump\", \"time\":200}");

                var stats = x.getStats();
                return stats.Equals("[{\"action\":\"jump\",\"avg\":150.0},{\"action\":\"run\",\"avg\":75.0}]");
            });

            //Test adding a single action
            tester.DoTest("Single Action", x =>
            {
                return true == string.IsNullOrEmpty(x.addAction("{\"action\":\"jump\",\"time\":100}"));
            });

            //Test adding many different actions other than the 2 that were given
            tester.DoTest("Add Many Different Actions", x =>
            {
                var t1 = x.addAction("{\"action\":\"jump\",\"time\":100}");
                var t2 = x.addAction("{\"action\":\"jump\", \"time\":200}");
                var t3 = x.addAction("{\"action\":\"run\", \"time\":300}");
                var t4 = x.addAction("{\"action\":\"run\", \"time\":200}");
                var t5 = x.addAction("{\"action\":\"fly\", \"time\":350}");
                var t6 = x.addAction("{\"action\":\"fly\", \"time\":150}");
                var t7 = x.addAction("{\"action\":\"fall\", \"time\":400}");
                var t8 = x.addAction("{\"action\":\"fall\", \"time\":1000}");
                var t9 = x.addAction("{\"action\":\"speak\", \"time\":100}");
                var t10 = x.addAction("{\"action\":\"speak\", \"time\":200}");
                var t11 = x.addAction("{\"action\":\"sit\", \"time\":500}");
                var t12 = x.addAction("{\"action\":\"sit\", \"time\":100}");
                var t13 = x.addAction("{\"action\":\"stand\", \"time\":400}");
                var t14 = x.addAction("{\"action\":\"stand\", \"time\":200}");

                return true == string.IsNullOrEmpty(t1)
                    && true == string.IsNullOrEmpty(t2)
                    && true == string.IsNullOrEmpty(t3)
                    && true == string.IsNullOrEmpty(t4)
                    && true == string.IsNullOrEmpty(t5)
                    && true == string.IsNullOrEmpty(t6)
                    && true == string.IsNullOrEmpty(t7)
                    && true == string.IsNullOrEmpty(t8)
                    && true == string.IsNullOrEmpty(t9)
                    && true == string.IsNullOrEmpty(t10)
                    && true == string.IsNullOrEmpty(t11)
                    && true == string.IsNullOrEmpty(t12)
                    && true == string.IsNullOrEmpty(t13)
                    && true == string.IsNullOrEmpty(t14);

            });

            //Test getting stats for all of the different actions that are added
            tester.DoTest("Get Stats From Many Different Actions", x =>
            {
                var t1 = x.addAction("{\"action\":\"jump\",\"time\":100}");
                var t2 = x.addAction("{\"action\":\"jump\", \"time\":200}");
                var t3 = x.addAction("{\"action\":\"run\", \"time\":300}");
                var t4 = x.addAction("{\"action\":\"run\", \"time\":200}");
                var t5 = x.addAction("{\"action\":\"fly\", \"time\":350}");
                var t6 = x.addAction("{\"action\":\"fly\", \"time\":150}");
                var t7 = x.addAction("{\"action\":\"fall\", \"time\":400}");
                var t8 = x.addAction("{\"action\":\"fall\", \"time\":1000}");
                var t9 = x.addAction("{\"action\":\"speak\", \"time\":100}");
                var t10 = x.addAction("{\"action\":\"speak\", \"time\":200}");
                var t11 = x.addAction("{\"action\":\"sit\", \"time\":500}");
                var t12 = x.addAction("{\"action\":\"sit\", \"time\":100}");
                var t13 = x.addAction("{\"action\":\"stand\", \"time\":400}");
                var t14 = x.addAction("{\"action\":\"stand\", \"time\":200}");

                var stats = x.getStats();

                return stats.Equals("[{\"action\":\"fall\",\"avg\":700.0},{\"action\":\"fly\",\"avg\":250.0},{\"action\":\"jump\",\"avg\":150.0},{\"action\":\"run\",\"avg\":250.0},{\"action\":\"sit\",\"avg\":300.0},{\"action\":\"speak\",\"avg\":150.0},{\"action\":\"stand\",\"avg\":300.0}]");

            });

            //Test the ability of the code to handle malformed JSON
            tester.DoTest("Malformed JSON Inputs", x =>
            {
                var t1 = x.addAction("{\"actison\":\"jump\",\"time\":100}");
                var t2 = x.addAction("{\"action\":\"run\", \"tiome\":75}");
                var t3 = x.addAction("{\"action\":, \"time\":200}");
                return false == string.IsNullOrEmpty(t1)
                    && false == string.IsNullOrEmpty(t2)
                    && false == string.IsNullOrEmpty(t3);
            });

            //Test assumption that an action shouldn't have a negative time value
            tester.DoTest("Negative Time Value", x =>
            {
                return false == string.IsNullOrEmpty(x.addAction("{\"action\":\"jump\",\"time\":-1}"));
            });

            //Test the ability of the code to handle concurrent calls safely
            await tester.DoTestAsync("Concurrent Calls", async x => {

                await Task.WhenAll(
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":100}")),
                    Task.Run(() => x.addAction("{\"action\":\"run\",\"time\":200}")),
                    Task.Run(() => x.getStats()),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":300}")),
                    Task.Run(() => x.addAction("{\"action\":\"run\",\"time\":100}")),
                    Task.Run(() => x.addAction("{\"action\":\"run\",\"time\":200}")),
                    Task.Run(() => x.getStats()),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":300}")),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":100}")),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":100}"))
                );

                var stats = x.getStats();

                return stats.Equals("[{\"action\":\"jump\",\"avg\":180.0},{\"action\":\"run\",\"avg\":166.66666666666666}]");

            });

            await tester.DoTestAsync("Concurrent Calls With Get Stats", async x => {
            
                await Task.WhenAll(
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":100}")),
                    Task.Run(() => x.addAction("{\"action\":\"run\",\"time\":200}")),
                    Task.Run(() => x.getStats()),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":300}")),
                    Task.Run(() => x.addAction("{\"action\":\"run\",\"time\":100}")),
                    Task.Run(() => x.getStats()),
                    Task.Run(() => x.addAction("{\"action\":\"run\",\"time\":200}")),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":300}")),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":100}")),
                    Task.Run(() => x.getStats()),
                    Task.Run(() => x.addAction("{\"action\":\"jump\",\"time\":100}"))
                );
                var stats = x.getStats();
            
                return stats.Equals("[{\"action\":\"jump\",\"avg\":180.0},{\"action\":\"run\",\"avg\":166.66666666666666}]");
            
            });
        }

        public class ActionTester
        {
            public void DoTest(string testTitle, Func<ActionAdder, bool> doTest)
            {
                var title = testTitle.ToUpper();

                var service = new ActionAdder();
                Console.WriteLine($"{DateTime.Now:g} - [TEST][{title}] Started");
                var testResult = doTest.Invoke(service);
                Console.WriteLine($"{DateTime.Now:g} - [TEST][{title}] Finished");
                Console.WriteLine($"{DateTime.Now:g} - [TEST][{title}] Results = {(testResult ? "Passed" : "---FAILED---")}");
            }

            public async Task DoTestAsync(string testTitle, Func<ActionAdder, Task<bool>> doTest)
            {
                var title = testTitle.ToUpper();

                var service = new ActionAdder();
                Console.WriteLine($"{DateTime.Now:g} - [TEST][{title}] Started");
                var testResult = await doTest.Invoke(service);
                Console.WriteLine($"{DateTime.Now:g} - [TEST][{title}] Finished");
                Console.WriteLine($"{DateTime.Now:g} - [TEST][{title}] Results = {(testResult ? "Passed" : "FAILED")}");
            }

        }
    }
}
