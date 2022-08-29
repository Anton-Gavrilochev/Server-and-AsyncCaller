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
                Thread.Sleep(1000); //Имитация задержки чтения
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
                Thread.Sleep(10000); //Имитация задержки записи
                count = value;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }
    }
}

