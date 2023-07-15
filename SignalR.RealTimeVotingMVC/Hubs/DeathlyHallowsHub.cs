using Microsoft.AspNetCore.SignalR;
using SignalR.RealTimeVotingMVC.Data;

namespace SignalR.RealTimeVotingMVC.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetVoteStatus()
        {
              return VotingSystem.VoteCounts;
        }
    }
}
