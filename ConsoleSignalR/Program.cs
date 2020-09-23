using System;
using System.ComponentModel.Design;
using System.Linq;

namespace ConsoleSignalR
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var client = new Client();

            while (true)
            {
                var input = Console.ReadLine();
                var isbreak = false;
                switch (input)
                {
                    case "/e":
                        client.Disconnect(); 
                        isbreak = true;
                        break;
                    case "/c":
                        client.Connect();
                        break;
                    case "/h":
                        Help();
                        break;
                    case "/d":
                        client.Disconnect();
                        break;
                    default:
                        if (input.StartsWith("/n"))
                            client.Name(input.Substring(3));
                        else client.Send(input);
                        break;
                }
                if (isbreak) break;
            }
        }

        static public void Help()
        {
            Console.WriteLine("command list:\n/h help\n/e exit\n/c connect\n/d disconnect\n/n change name");
        }
    }
}
