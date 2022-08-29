using System;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
    class Program
    {
        private static event EventHandler myEventHandler = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Выберите задачу: \n(1) - count на \"сервере\"\n(2) - Полусинхронный вызов");
            try
            {
                int number = Int32.Parse(Console.ReadLine());
                switch (number)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    default: throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Такого задания нет!");
            }
        }

        private static void Task2()
        {
            EventHandler h = new EventHandler(myEventHandler);

            AsyncCaller ac = new AsyncCaller(h);

            bool completedOK = ac.Invoke(5000, null, EventArgs.Empty);
        }


        private static void Task1()
        {
            Thread thread1 = new Thread(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    ReadFromServer(1);
                }
            });

            Thread thread2 = new Thread(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    ReadFromServer(2);
                }
            });

            Thread thread3 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(10000);
                    int count = new Random().Next(1, 100);
                    WriteFromServer(1, count);
                }
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        private static void ReadFromServer(int ThreadNumber)
        {
            Thread.Sleep(2000); //Имитация частоты запроса
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss") + " | " + "Читатель " + ThreadNumber + " пытается получить значение значение...");
            int s = Server.GetCount();
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss") + " | " + "Читатель " + ThreadNumber + " получил значение: " + "\"" + s + "\"");
        }

        private static void WriteFromServer(int ThreadNumber, int count)
        {
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss") + " | " + "Писатель " + ThreadNumber + " записывает новое значение: " + "\"" + count + "\"");
            Server.AddToCount(count);
            Console.WriteLine(DateTime.Now.ToString("H:mm:ss") + " | " + "Писатель " + ThreadNumber + " записал новое значение: " + "\"" + count + "\"");

        }
    }
}
