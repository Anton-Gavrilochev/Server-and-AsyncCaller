using System;
using System.Threading;

namespace Server
{
    public class AsyncCaller
    {
        private EventHandler eventHandler;
        public AsyncCaller(EventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public bool Invoke(int timeOut, object sender, EventArgs e)
        {
            Thread thread = new Thread(() => { eventHandler.Invoke(sender, e); });
            thread.Start();
            return thread.Join(timeOut);
        }
    }
}