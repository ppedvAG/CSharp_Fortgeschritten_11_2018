using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {

        const string Server_URL = "http://localhost:8080";
        string _username;

        public Form1()
        {
            InitializeComponent();
        }

        IHubProxy _proxy;

        private void button1_Click(object sender, EventArgs e)
        {
            _username = textBox1.Text;

            var connection = new HubConnection(Server_URL);
           
            _proxy = connection.CreateHubProxy("ChatHub");
            _proxy.On<ChatNachricht>("ShowMessage", empfangeNachricht);
            connection.Start();


        }
        


        private void empfangeNachricht(ChatNachricht nachrichtVomServer)
        {
            listBox1.Invoke(new Action(() => { listBox1.Items.Add($"{nachrichtVomServer.Sender}: {nachrichtVomServer.Inhalt}"); }
            ));
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nachricht = textBox2.Text;
            _proxy.Invoke("SendeNachricht", new ChatNachricht() { Sender=_username, Inhalt=nachricht} );
        }
    }

    public class ChatNachricht
    {
        public string Sender { get; set; }
        public string Inhalt { get; set; }
    }
}
