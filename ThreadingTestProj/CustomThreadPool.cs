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
            }

            _actions = new Queue<Action>(); 
        }
        public void Queue(Action action)
        {
            _actions.Enqueue(action);
        }

        private  void ThreadProc()
        {
            while (true)
            {
                if(_actions.Count > 0)
                {
                    var action = _actions.Dequeue();
                    action();
                }
            }
        } 
    }
}
