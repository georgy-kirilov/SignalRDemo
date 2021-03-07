namespace SignalRDemo.Client
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/chathub").Build();
            connection.StartAsync().Wait();
            connection.On("ReceiveMessage", (string username, string message) => ReceiveMessage(username, message));
            connection.On("AnnounceJoinRoom", (string username) =>
            {
                Console.WriteLine($"{username} joined the group");
            });

            Console.WriteLine("Enter your username");
            string username = Console.ReadLine();

            while (true)
            {
                string message = Console.ReadLine();
                await SendMessageAsync(connection, username, message);

                var messageParts = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (messageParts[0].ToLower() == "join")
                {
                   await connection.InvokeCoreAsync("JoinRoom", new string[] { messageParts[1], username });
                }

                if (message.Trim().Length == 0)
                {
                    break;
                }
            }
        }

        public static async Task SendMessageAsync(HubConnection connection, string username, string message)
        {
            await connection.InvokeCoreAsync("SendMessage", new string[] { username, message });
        }

        public static void ReceiveMessage(string username, string message)
        {
            Console.WriteLine($"{username}: {message}");
        }
    }
}