using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ThreadingTestProj.DeadLocks
{
    public class DeadLockCodeSemple
    {
        object o1 = new object();
        object o2 = new object();

        public void Foo()
        {
            Console.WriteLine("Starting");

            var task1 = Task.Run(() =>
            {
                lock (o1)
                {
                    Thread.Sleep(1000);
                    lock (o2)
                    {
                        Console.WriteLine("Finished thread");
                    }
                }
            });

            var task2 = Task.Run(() =>
            {
                lock (o1)
                {
                    Thread.Sleep(1000);
                    lock (o1)
                    {
                        Console.WriteLine("Finished thread");
                    }
                }
            });

            Task.WaitAll(task1, task2);
            Console.WriteLine("Finished");
        }


        public static void SetTimer(System.Timers.Timer aTimer)
        {
            // Create a timer with a two second interval.
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }

   
}
