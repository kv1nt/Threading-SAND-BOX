using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTestProj
{
    class Program
    {


        static void Main(string[] args)
        {
            var obj = new LockDiplay();
            Thread thread1 = new Thread(obj.FileReader);
            Thread thread3 = new Thread(obj.FileWriter);
            Thread thread2 = new Thread(obj.FileReader);

            thread1.Start();
            thread3.Start();
            thread2.Start();

            Console.ReadKey();
        }

    }

    public class LockDiplay
    {
        
        static object obj = new object();
        public void DisplayNum()
        {
            lock (this)
            {
                for (int i = 1; i < 11; i++)
                {
                    Thread.Sleep(200);
                    Console.WriteLine($"i = {i}");
                }
            }

            Console.WriteLine("--------------------------------");
        }

        public void FileReader()
        {
            string text = File.ReadAllText(@"C:\Users\vipalamarchuk\Desktop\Threading\Threading-SAND-BOX\ThreadingTestProj\test.txt");
            Console.WriteLine(text);
        }

        public void FileWriter()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\vipalamarchuk\Desktop\Threading\Threading-SAND-BOX\ThreadingTestProj\test.txt"))
            {
                sw.WriteLine("Vitalii");
            }
            
        }
    }
}
