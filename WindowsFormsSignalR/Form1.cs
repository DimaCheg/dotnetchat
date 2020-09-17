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

        private void button1_Click(object sender, EventArgs e0)
        {
            HubConnection = new HubConnectionBuilder().WithUrl("/chat").Build();
            HubConnection.On("Send", (string message) => textBox2.AppendText(message + "\n"));
            HubConnection.StartAsync();
            button1.Enabled = false;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e0)
        {
            HubConnection.InvokeAsync("Send", textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e0)
        {
            HubConnection.DisposeAsync();
            button3.Enabled = false;
            button1.Enabled = true;
        }
    }
}
