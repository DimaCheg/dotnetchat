using System;
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
                    textBox2.AppendText(name + " says " + message + Environment.NewLine));
            await HubConnection.StartAsync();
            button1.Enabled = false;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e0)
        {
            HubConnection.InvokeAsync("SendMessage", "Yasha", textBox1.Text);
        }

        private async void button3_Click(object sender, EventArgs e0)
        {
            await HubConnection.DisposeAsync();
            button3.Enabled = false;
            button1.Enabled = true;
        }
    }
}
