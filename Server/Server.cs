using System.Threading;

namespace Server
{
    static class Server
    {
        private static ReaderWriterLock rwl = new ReaderWriterLock();
        private static int count;

        public static int GetCount()
        {
            rwl.AcquireReaderLock(100000);
            try
            {
                return count;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }

        public static void AddToCount(int value)
        {
            rwl.AcquireWriterLock(100000);
            try
            {
                count = value;
            }
            finally
            {
                Thread.Sleep(5000);
                rwl.ReleaseWriterLock();
            }
        }
    }
}

