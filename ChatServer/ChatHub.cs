using Microsoft.AspNet.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{

    public class ChatNachricht
    {
        public string Sender { get; set; }
        public string Inhalt { get; set; }
    }


    //Kommunikationszentrale: Interaktion mit dem Server
    public class ChatHub : Hub
    {

        Mutex mutex = new Mutex();

        //Können vom Client aufgerufen werden
        public void SendeNachricht(ChatNachricht nachricht)
        {
            //Anderer Prozess

            //Evtl. greifen mehrere Clients gleichzeitig auf die AddMessage Methode zu.
            //Mittels Mutex wird sichergestellt, das immer nur ein Thread die AddMessage-Methode gleichzeitig ausführt.
            mutex.WaitOne();
            Console.WriteLine($"{nachricht.Sender}: {nachricht.Inhalt}");
            mutex.ReleaseMutex();
            //Show-Message Methode aller verbundenen Clients aufrufen
           
            Clients.All.ShowMessage(nachricht);
        }

        public override Task OnConnected()
        {
            Console.WriteLine($"Client mit ID {Context.ConnectionId} hat sich verbunden");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine($"Client mit ID {Context.ConnectionId} hat sich abgemeldet");
            return base.OnDisconnected(stopCalled);
        }
    }
}