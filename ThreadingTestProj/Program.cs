using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTestProj
{
    class Program
    {
        static object locker = new object();
        static string[] Names = new string[10];
        
        const string path = @"C:\Users\vipalamarchuk\Desktop\Threading\Threading-SAND-BOX\ThreadingTestProj\test.txt";

        static unsafe void Main(string[] args)
        {
            Console.WriteLine("Do work main...Started");
            new Thread(() => FileWriter("Hello world1")).Start();
            new Thread(() => FileWriter("Hello world2")).Start();
            var th = new Thread(() => FileReader());
            th.Start();
            th.Join();
            //Task.Run(() =>  FileWriter("Hello world1"));
            //Task.Run(() => FileWriter("Hello world2"));
            //Task.Run(FileReader);

            Console.WriteLine("Do work main...Ended");

            Console.ReadKey();
        }

        static ReaderWriterLockSlim lockSlim1 = new ReaderWriterLockSlim();

        static ReaderWriterLockSlim lockSlim2 = new ReaderWriterLockSlim();

        public static void FileReader()
        {
            byte[] buff = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            lockSlim2.EnterWriteLock();
            try
            {
                using (var stream =  File.OpenRead(path))
                {
                    if (stream.Read(buff,0,buff.Length) > 0)
                    {
                        Console.WriteLine(temp.GetString(buff));
                    }
                    
                }
            }
            finally
            {

                lockSlim2.ExitWriteLock();
            }
        }
        
        public static void FileWriter(string str)
        {
            lockSlim2.EnterWriteLock();

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(str);

                using (var stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    stream.Write(data);
                }

            }
            finally
            {
                lockSlim2.ExitWriteLock();
            }
            
        }

    }

    
}
