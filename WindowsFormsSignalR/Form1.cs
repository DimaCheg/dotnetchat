using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace WindowsFormsSignalR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public HubConnection HubConnection;

        private async void button1_Click(object sender, EventArgs e0)
        {
            HubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chatHub").Build();
            HubConnection.On("ReceiveMessage",
                (string name, string message) =>
                {
                    var data = new ObservableCollection<string> {name + " " + message};
                    data.Subscribe(new AnonymousObserver<string>(abc =>
                    {
                        var newData = abc.Split();
                        textBox2.AppendText(newData[0] + " says " + newData[1] + Environment.NewLine);
                    }));
                });
            var task = HubConnection.StartAsync();
            button1.Enabled = false;
            button3.Enabled = true;
            await task;
        }

        private void button2_Click(object sender, EventArgs e0)
        {
            HubConnection.InvokeAsync("SendMessage", "Yasha", textBox1.Text);
        }

        private async void button3_Click(object sender, EventArgs e0)
        {
            var task = HubConnection.DisposeAsync();
            button3.Enabled = false;
            button1.Enabled = true;
            await task;
        }
    }
}
