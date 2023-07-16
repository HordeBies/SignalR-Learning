using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.SampleMVC.Data;

namespace SignalR.SampleMVC.Hubs
{
    public class BasicChatHub(ApplicationDbContext db) : Hub
    {
        [Authorize]
        public async Task SendMessage(string sender, string? receiver, string message)
        {
            if(string.IsNullOrWhiteSpace(receiver))
                await Clients.All.SendAsync("MessageReceived", sender, message);
            else
            {
                var receiverId = db.Users.FirstOrDefault(u => u.UserName == receiver)?.Id ?? throw new InvalidOperationException("User not found.");
                await Clients.User(receiverId).SendAsync("MessageReceived", sender, message);
            }
        }
    }
}
