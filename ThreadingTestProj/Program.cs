using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadingTestProj.DeadLocks;
using ThreadingTestProj.Threading;

namespace ThreadingTestProj
{
    class Program
    {
        private static readonly System.Timers.Timer aTimer = new System.Timers.Timer(1000);
        static unsafe void Main(string[] args)
        {
            Console.WriteLine("Do work main...Started");

            DeadLockCodeSemple deadLockCodeSemple = new DeadLockCodeSemple();
            DeadLockCodeSemple.SetTimer(aTimer);
            deadLockCodeSemple.Foo();
            aTimer.Stop();
            aTimer.Dispose();
            aTimer.Close();
            //ThreadCodeSemple ts = new ThreadCodeSemple();
            //Thread.Sleep(100);
            //Task.Run(() => ts.PrintWord("Thread 1!!!", 500));
            //Task.Run(() => ts.PrintNumber(7, 1000));

            //var dayOfWeek = Task.Run(() => DateTime.Today.DayOfWeek );
            //dayOfWeek.ContinueWith(x => Console.WriteLine(x.Result));


            //Task.Run(() => ts.PrintWord("Thread      3!!!", 1000));

            Console.WriteLine("Do work main...Ended");

            Console.ReadKey();
        }


    }
    
}
