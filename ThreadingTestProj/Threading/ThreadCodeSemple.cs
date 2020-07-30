using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTestProj.Threading
{
    public class ThreadCodeSemple
    {
        object blokObj1 = new object();
        object blokObj2 = new object();

        public void PrintWord(string val, int timeOut)
        {
            lock (blokObj1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(val);
                    Thread.Sleep(timeOut);
                }
            }
        }

        public void PrintNumber(int val, int timeOut)
        {
            lock (blokObj1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(val);
                    Thread.Sleep(timeOut);
                }
            }
        }

        public string PrintText()
        {

            return "This is some text ...";
        }
    }
}
