using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleSignalR
{
    class Client
    {
        private string name = "unknown";
        private string url = "https://localhost:44384/chatHub";
        public HubConnection HubConnection;
        private bool isConnect = false;

        public Client(){}

        public void Name(string name)
        {
            this.name = name;
        }

        public async void Connect()
        {
            if (isConnect) return;
            HubConnection = new HubConnectionBuilder().WithUrl(url).Build();
            isConnect = true;
            HubConnection.On("ReceiveMessage",
                (string name, string message) =>
                    Console.WriteLine(name + " says " + message + Environment.NewLine));
            await HubConnection.StartAsync();
        }

        public void Send(string message)
        {
            HubConnection.InvokeAsync("SendMessage", name, message);
        }

        public async void Disconnect()
        {
            if (!isConnect) return;
            await HubConnection.DisposeAsync();
            isConnect = false;
        }
    }
}
