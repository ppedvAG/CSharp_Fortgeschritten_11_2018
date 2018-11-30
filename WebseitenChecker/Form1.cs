using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebseitenChecker
{
    public partial class Form1 : Form
    {

        string _url = "http://www.fette-compacting.com";

        List<string> _urls = new List<string>()
        {
         "http://www.fette-compacting.com",
        "http://www.fette-compacting.com",
         "http://www.cnn.com",
          "http://www.youtube.com",
           "http://www.askdoaksodkasodksaodk.com",
            "http://www.google.com"
            };

        public Form1()
        {
            InitializeComponent();
        }

        CancellationTokenSource _cts;

        private void button1_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            Task t = new Task(() =>
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        _cts.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(3000);
                        _cts.Token.ThrowIfCancellationRequested();
                        string result = client.GetStringAsync(_url).Result;

                    }
                    catch (OperationCanceledException)
                    {
                        InvokeMethod(() => label1.Text = "Prüfung abgebrochen!");
                        return;
                    }
                    catch (Exception)
                    {
                        InvokeMethod(() => label1.Text = "Website ist kaputt!");
                        return;
                    }
                    InvokeMethod(() => label1.Text = "Website geht");
                });
            t.Start();
        }

        public void InvokeMethod(Action action)
        {

            label1.Invoke(action);
        }

        public int InvokeFunc(Func<int> func)
        {
            return (int)label1.Invoke(func);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
            label1.Text = "Prüfung abgebrochen!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();

            listBox1.Items.Clear();

            Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(_urls, url =>
                {
                    int index = default;
                    try
                    {

                        index = InvokeFunc(() => {
                            return listBox1.Items.Add($"{url} ...");
                        });
                        HttpClient client = new HttpClient();
                        _cts.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(3000);
                        _cts.Token.ThrowIfCancellationRequested();
                        string result = client.GetStringAsync(url).Result;
                    }
                    catch (OperationCanceledException)
                    {
                        InvokeMethod(() => listBox1.Items[index] =  $"{url} wurde Abgebrochen");
                        return;
                    }
                    catch (Exception exp)
                    {
                        InvokeMethod(() => listBox1.Items[index] =  $"{url} ist kaputt weil {exp.Message}");
                        return;
                    }
                    InvokeMethod(() => listBox1.Items[index] = $"{url} geht!");
                });
            });
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();

            listBox1.Items.Clear();

            foreach (var url in _urls)
            {
                listBox1.Items.Add($"{url} ...");
            }

            Task.Factory.StartNew(() =>
            {
                Parallel.For(0, _urls.Count, index =>
                {
                    string url = _urls[index];
                    try
                    {
                        HttpClient client = new HttpClient();
                        _cts.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(3000);
                        _cts.Token.ThrowIfCancellationRequested();
                        string result = client.GetStringAsync(_urls[index]).Result;
                    }
                    catch (OperationCanceledException)
                    {
                        InvokeMethod(() => listBox1.Items[index] = $"{url} wurde Abgebrochen");
                        return;
                    }
                    catch (Exception exp)
                    {
                        InvokeMethod(() => listBox1.Items[index] = $"{url} ist kaputt weil {exp.Message}");
                        return;
                    }
                    InvokeMethod(() => listBox1.Items[index] = $"{url} geht!");
                });
            });

        }

        private void button4_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();

            listBox1.Items.Clear();

            foreach (var url in _urls)
            {
                listBox1.Items.Add($"{url} ...");
            }

            for (int i = 0; i < _urls.Count; i++)
            {
                CheckeWebsiteUndSchreibeErgebnisInListBoxAsync(_urls[i], i);
            }
        }

        private async void CheckeWebsiteUndSchreibeErgebnisInListBoxAsync(string url, int index)
        {
            try
            {
                HttpClient client = new HttpClient();
                _cts.Token.ThrowIfCancellationRequested();
                await Task.Delay(3000);
                _cts.Token.ThrowIfCancellationRequested();
                string result =  await client.GetStringAsync(_urls[index]);
            }
            catch (OperationCanceledException)
            {
                InvokeMethod(() => listBox1.Items[index] = $"{url} wurde Abgebrochen");
                return;
            }
            catch (Exception exp)
            {
                listBox1.Items[index] = $"{url} ist kaputt weil {exp.Message}";
                return;
            }
            listBox1.Items[index] = $"{url} geht!";
        }
    }
}
