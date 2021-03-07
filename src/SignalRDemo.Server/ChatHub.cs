namespace SignalRDemo.Server
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
