using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTestProj
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Start Main Thread");

            //new Thread(
            //    () => Que_Helper.FillToQueueValues()
            //    ).Start();

            Thread thread1 = new Thread(Que_Helper.FillToQueueValues);
            Thread thread2 = new Thread(Que_Helper.AddFromEnotherThread);
            thread1.Start();
            Thread.Sleep(1000);
            thread2.Start();
         

            Que_Helper.PrintQueue(); 


            Console.WriteLine("End Main Thread - EXIT");
        }

       
    }

    public static class Que_Helper
    {
        public static List<object> GlobalQueue { get; private set; }

        static Que_Helper()
        {
            GlobalQueue = new List<object>();
        }

        public static void PrintQueue()
        {
            foreach (var item in GlobalQueue)
            {
                Console.WriteLine(item);
            }
        }

        public static void FillToQueueValues()
        {
           
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(200);
                    GlobalQueue.Add(1);
                }
            
        }

        public static void AddFromEnotherThread()
        {
           
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(200);
                    GlobalQueue.Add(222);
                }
            
        }
    }
}
