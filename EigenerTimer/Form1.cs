using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EigenerTimer
{
    public partial class Form1 : Form
    {
        private int _countdown;

        //private BessererTimer _timer = new BessererTimer();

        public int Countdown
        {
            get { return _countdown; }
            set
            {
                _countdown = value;
                if (label1 != null)
                    if (label1.InvokeRequired)
                    {
                        label1.Invoke(new Action(() =>
                        {
                            label1.Text = _countdown.ToString();
                        }));
                    }
                    else
                    {
                        label1.Text = _countdown.ToString();
                    }
            }
        }




        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Countdown--;
            Thread.Sleep(500);
            if (Countdown <= 0)
            {
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Countdown = 10;
            //timer1.Start();
            while (Countdown > 0)
            {
                Thread.Sleep(1000);
                Countdown--;
                Application.DoEvents();
            }
        }

        BessererTimer _bessererTimer;

        private void button2_Click(object sender, EventArgs e)
        {
            Countdown = 10;
            _bessererTimer = new BessererTimer(() =>
            {
                Thread.Sleep(500);
                Countdown--;
            }, 1000, true, 10, true);
            _bessererTimer.AufrufendesForm = this;

            _bessererTimer.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _bessererTimer?.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _bessererTimer?.Stop();

        }
    }
}
