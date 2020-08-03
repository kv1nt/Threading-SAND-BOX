using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadingTestProj
{
    public class CustomThreadPool
    {
        //https://www.youtube.com/watch?v=OxME9xdvugY
        private readonly Thread[] _threads;
        private readonly Queue<Action> _actions;
        private readonly object _syncRoot;

        public CustomThreadPool(int maxThreads = 4)
        {
            _threads = new Thread[maxThreads];
            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i] = new Thread(ThreadProc)
                {
                    IsBackground = true,
                    Name = $"Custom thread poll Threads {i}"
                };
                _threads[i].Start();
            }

            _actions = new Queue<Action>();
            _syncRoot = new object();
        }
        public void Queue(Action action)
        {
            Monitor.Enter(_syncRoot);
            try
            {
                _actions.Enqueue(action);
                if(_actions.Count == 1)
                {
                    Monitor.Pulse(_syncRoot);
                }
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }

        private  void ThreadProc()
        {
            
            while (true)
            {
                Action action;
                Monitor.Enter(_syncRoot);
                try
                {
                    if (_actions.Count > 0)
                    {
                        action = _actions.Dequeue();

                    }
                    else
                    {
                        Monitor.Wait(_syncRoot);
                        continue;
                    }
                }
                finally
                {
                    Monitor.Exit(_syncRoot);
                }

                action();
            }
        } 
    }
}
