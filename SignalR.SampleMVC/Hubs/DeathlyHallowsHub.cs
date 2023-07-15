using Microsoft.AspNetCore.SignalR;
using SignalR.SampleMVC.Data;

namespace SignalR.SampleMVC.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetVoteStatus()
        {
              return VotingSystem.VoteCounts;
        }
    }
}
