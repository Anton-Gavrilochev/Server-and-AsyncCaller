using System;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    string s = Server.GetCount().ToString();
                    Console.WriteLine(DateTime.Now.ToString("H:mm:ss")+ ": count = " + s + " поток №1");
                }
            });
            Thread thread2 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    string s = Server.GetCount().ToString();
                    Console.WriteLine(DateTime.Now.ToString("H:mm:ss") + ": count = " + s + " поток №2");
                }
            });
            Thread thread3 = new Thread(() => 
            {
                Thread.Sleep(5000);
                int i = new Random().Next(1, 100);
                Server.AddToCount(i); 
                Console.WriteLine(DateTime.Now.ToString("H:mm:ss") + ": " + "данные изменены ("+i+"), поток №3"); 
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Console.ReadKey();
        }
    }
}
