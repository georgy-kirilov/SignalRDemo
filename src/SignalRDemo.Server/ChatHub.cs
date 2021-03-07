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

        public async Task JoinRoom(string roomName, string username)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await this.Clients.Group(roomName).SendAsync("AnnounceJoinRoom", username);
        }
    }
}
