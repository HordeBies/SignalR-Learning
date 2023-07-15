using Microsoft.AspNetCore.SignalR;

namespace SignalR.HogwartsHousesMVC.Hubs
{
    public class HousesHub : Hub
    {
        public static List<string> GroupsJoined { get; set; } = new List<string>();
        private string getName(string houseName) => Context.ConnectionId + ":" + houseName;
        private string getHouseList() => string.Join(" ", GroupsJoined.Where(r => r.StartsWith(Context.ConnectionId)).Select(r => r.Split(':')[1] + " "));
        public async Task JoinHouse(string houseName)
        {
            if (!GroupsJoined.Contains(getName(houseName)))
            {
                GroupsJoined.Add(getName(houseName));


                await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
                await Clients.Caller.SendAsync("subscriptionStatus", getHouseList(), houseName, true);
                await Clients.OthersInGroup(houseName).SendAsync("userJoined", houseName, "");
            }
        }
        public async Task LeaveHouse(string houseName)
        {
            if (GroupsJoined.Contains(getName(houseName)))
            {
                GroupsJoined.Remove(getName(houseName));

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
                await Clients.Caller.SendAsync("subscriptionStatus", getHouseList(), houseName, false);
                await Clients.OthersInGroup(houseName).SendAsync("userLeaved", houseName, "");
            }
        }

        public async Task SendNotification(string houseName, string message)
        {
            await Clients.Group(houseName).SendAsync("houseNotification", "", houseName, message);
        }
    }
}
