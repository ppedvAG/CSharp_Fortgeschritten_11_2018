using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace EigenerTimer
{
    public class BessererTimer
    {
        //wie viele Durchläufe
        public int MaxDurchläufe { get; set; }
        //Hintergrund/Vordergrund
        public bool ImHintergrund { get; set; }

        public int Interval { get; set; }

        public bool AllesAutomatischInvoken { get; set; } = false;

        //auszuführende Methode bei jedem "Tick" (Interval abgelaufen)
        private Action _action;
        private Thread _thread;

        public Form AufrufendesForm { get; set; }

        private CancellationTokenSource _cts;

        //Bonus:
        //Timer unterbrechen/fortsetzen


        public BessererTimer(Action action, int interval, bool imHintergrund = true, int maxLimit = 1000, bool automatischerInvoke = false)
        {
            MaxDurchläufe = maxLimit;
            Interval = interval;
            _action = action;
            ImHintergrund = imHintergrund;
            AllesAutomatischInvoken = automatischerInvoke;
        }

        

        public void Start()
        {

            //Main_Thread
            var dispatcherDesAktuellenThreads = Dispatcher.CurrentDispatcher;

            //Timer-Thread starten
            _thread = new Thread(() =>
            {
                try
                {
                    for (int i = 0; i < MaxDurchläufe; i++)
                    {
                        _cts.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(Interval);
                        _cts.Token.ThrowIfCancellationRequested();

                        if (AllesAutomatischInvoken)
                        {
                            
                            dispatcherDesAktuellenThreads.Invoke(new Action(() =>
                            {
                                _action?.Invoke();
                            }));
                        }
                        else
                        {
                            _action?.Invoke();
                        }



                        _cts.Token.ThrowIfCancellationRequested();
                    }
                }
                //OperationCancledException wird von _cts.Token.ThrowIfCancellationRequested() geworfen
                catch (OperationCanceledException exp)
                {
                    //Aufräumarbeiten bei Abbruch des Threads...
                }
                catch (Exception exp)
                {
                    //Aufräumarbeiten bei anderen Fehlerns...
                }

            });
            _cts = new CancellationTokenSource();
            _thread.IsBackground = ImHintergrund;
            _thread.Start();
        }

        public void Stop()
        {
            _cts?.Cancel();
        }
    }
}
