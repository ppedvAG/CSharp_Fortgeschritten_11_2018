using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsTasksAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadsStarten();
            //for (int i = 0; i < 5; i++)
            //{
            //ThreadWettrennen();

            //}
            //ThreadsAbbrechen();
            //TaskWettrennen();
            //ParallelNutzen();
            TasksVerketten();
            Console.WriteLine("Hauptthread ist zu Ende!");

            Console.ReadKey();
        }

        private static void TasksVerketten()
        {
            int result = 0;

            hörAuf = new CancellationTokenSource();
            Task task = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Küche ist vorbereitet");
                result = 10;
            },hörAuf.Token);

            task.ContinueWith(t => {
                Thread.Sleep(100);
                
                Console.WriteLine($"Kochen: {result}");
            },hörAuf.Token);

            task.Start();

            Console.WriteLine("Zum Abbrechen Key drücken");
            Console.ReadKey();


            hörAuf.Cancel();

            Console.WriteLine();
        }

        private static void ParallelNutzen()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<int> zahlen = new List<int>() { 2, 5, 10, 8, 7 };
            foreach (var item in zahlen)
            {
                //Thread.Sleep(2);
                Console.WriteLine(item * item);
            }
            watch.Stop();
            Console.WriteLine($"Dauer: {watch.ElapsedMilliseconds}");

            watch.Reset();
            watch.Start();
            var result = Parallel.ForEach(zahlen, (z, state) => {
                Thread.Sleep(z);
                Console.WriteLine(z * z);
                if(!state.IsStopped)
                {
                    state.Break();
                }
            });
            watch.Stop();
            long? bremser = result.LowestBreakIteration;

           
            if(bremser != null)
                Console.WriteLine($"Folgende Zahl hat das Break ausgelöst: {zahlen[(int)bremser]}");
            Console.WriteLine($"Dauer: {watch.ElapsedMilliseconds}");
            Parallel.For(0, 10, i => Console.WriteLine(i));

        }

        private static void TaskWettrennen()
        {
            Task<string> carlson = new Task<string>(() => {
                Thread.Sleep(1); MacheEtwasAufwändiges("Carlson", 100);
                return "Carlson";
            });
            Task<string> caruana = new Task<string>(() => {
                MacheEtwasAufwändiges("Caruana", 100);
                return "Caruana";
            });
            Task<string> hobbySpieler = new Task<string>(() => {
                MacheEtwasAufwändiges("Hobby", 120);
                return "Hobby";
            });

            List<Task<string>> taskListe = new List<Task<string>>()
            {
                carlson, caruana, hobbySpieler
            };

            foreach (var item in taskListe)
            {
                item.Start();
            }

            int sieger = Task.WaitAny(taskListe.ToArray());

            string siegerString = taskListe[sieger].Result;
           
            Console.WriteLine($"Sieger war ...{siegerString}");
        }

        private static void ThreadsAbbrechen()
        {
            Thread koch = new Thread(() => { MacheEtwasAufwändiges("Koch", 100); });
            hörAuf = new CancellationTokenSource();
            koch.Start();
            Console.WriteLine("Taste drücken für Notfall");
            Console.ReadKey();
            //Koch soll seine Arbeit unterbrechen
            //koch.Abort();
            hörAuf.Cancel();
            koch.Join();
            Console.WriteLine("Koch wurde unterbrochen");
        }

        private static string _sieger;

        private static void ThreadWettrennen()
        {
            _sieger = string.Empty;
            Thread carlson = new Thread(() => { Thread.Sleep(1);  MacheEtwasAufwändiges("Carlson", 100); });
            Thread caruana = new Thread(() => {  MacheEtwasAufwändiges("Caruana", 100); });

            carlson.Priority = ThreadPriority.Highest;
            caruana.Priority = ThreadPriority.Lowest;

            carlson.Start();
            caruana.Start();

           
            carlson.Join();
            //string sieger = "";
            //if(caruana.ThreadState != ThreadState.Stopped)
            //{
            //    sieger = "Carlson";
            //}
            //else
            //{
            //    sieger = "caruana";
            //}
            caruana.Join();
            Console.WriteLine($"Sieger war ...{_sieger}");
        }

        private static void ThreadsStarten()
        {
            Thread t1 = new Thread(() => { MacheEtwasAufwändiges("Thread 1", 100); });
            //Standardmäßig läuft jeder Thread im Vordergrund
            t1.IsBackground = true;
            t1.Start();

            Console.WriteLine("Nächste Anweisung im Hauptthread");
        }

        private static object lockDummy = true;
        private static object lockDummy2 = true;

        private static int zahl = 0;

        private static CancellationTokenSource hörAuf = new CancellationTokenSource();

        private static void MacheEtwasAufwändiges(string name, int sleepTime)
        {
            for (int i = 0; i < 10; i++)
            {
                if (hörAuf.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Ich räume noch schnell die Küche auf");
                    Thread.Sleep(1000);
                    Console.WriteLine("Bin fertig und unterbreche mich selbst");

                    return;
                }
                Console.WriteLine($"{name}: {i+1}. Zug");
                Thread.Sleep(sleepTime);
                if(hörAuf.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Ich räume noch schnell die Küche auf");
                    Thread.Sleep(1000);
                    Console.WriteLine("Bin fertig und unterbreche mich selbst");

                    return;
                }
            }

            Console.WriteLine($"{name} ist fertig!");

            #region Variante mit Montior.TryEnter
            //warte maximal 10 Millisekunden bis der Lock freigegeben ist
            //Konnte der Lock innerhalb der Wartezeit ergriffen werden? 
            //while (!Monitor.TryEnter(lockDummy, 0))
            //{
            //    Console.WriteLine($"Wartezeit hat nicht ausgereicht für {name}");
               
            //}
            //if (_sieger == string.Empty)
            //{
            //    Thread.Sleep(10);
            //    _sieger = name;
            //}
            //Monitor.Exit(lockDummy);
            #endregion

            //Kurzschreibweise für Montitor.Enter und Monitor.Exit
            lock (lockDummy)
            {
                if (_sieger == string.Empty)
                {
                    Thread.Sleep(10);
                    _sieger = name;
                }
            }
        }
    }
}
