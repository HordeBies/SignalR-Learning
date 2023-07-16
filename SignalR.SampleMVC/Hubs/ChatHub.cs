using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.SampleMVC.Data;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SignalR.SampleMVC.Hubs
{
    public class ChatHub(ApplicationDbContext db) : Hub
    {
        [Authorize]
        public async Task SendMessage(string sender, string? receiver, string message)
        {
            if (string.IsNullOrWhiteSpace(receiver))
                await Clients.All.SendAsync("MessageReceived", sender, message);
            else
            {
                var receiverId = db.Users.FirstOrDefault(u => u.UserName == receiver)?.Id ?? throw new InvalidOperationException("User not found.");
                await Clients.User(receiverId).SendAsync("MessageReceived", sender, message);
            }
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var userName = db.Users.FirstOrDefault(u => u.Id == userId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("UserConnected", userId, userName);

                HubConnections.AddUserConnection(userId, Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                if (HubConnections.HasUserConnection(userId, Context.ConnectionId))
                {
                    var userConnections = HubConnections.Users[userId];
                    userConnections.Remove(Context.ConnectionId);
                }
                var userName = db.Users.FirstOrDefault(u => u.Id == userId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("UserDisconnected", userId, userName);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewRoomCreated(int maxRoom, int roomId, string roomName)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = db.Users.FirstOrDefault(u => u.Id == userId).UserName;

            await Clients.All.SendAsync("NewRoomCreated", maxRoom, roomId, roomName, userId, userName);
        }
        public async Task RoomDeleted(int deletedRoomId, string deletedRoomName, int selectedRoomId)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = db.Users.FirstOrDefault(u => u.Id == userId).UserName;

            await Clients.All.SendAsync("RoomDeleted", deletedRoomId, deletedRoomName, selectedRoomId, userId, userName);
        }

        public async Task SendPublicMessage(int roomId, string roomName,string message)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = db.Users.FirstOrDefault(u => u.Id == userId).UserName;

            await Clients.All.SendAsync("PublicMessageReceived", roomId, roomName, userId, userName, message);
        }
        public async Task SendPrivateMessage(string receiverId, string receiverName, string message)
        {
            var senderId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var senderName = db.Users.FirstOrDefault(u => u.Id == senderId).UserName;

            await Clients.Users(senderId,receiverId).SendAsync("PrivateMessageReceived", senderId, senderName, receiverId, receiverName, message , Guid.NewGuid());
        }
    }
}
