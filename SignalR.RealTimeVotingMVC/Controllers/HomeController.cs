using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.RealTimeVotingMVC.Data;
using SignalR.RealTimeVotingMVC.Hubs;
using SignalR.RealTimeVotingMVC.Models;
using System.Diagnostics;

namespace SignalR.RealTimeVotingMVC.Controllers
{
    public class HomeController(IHubContext<DeathlyHallowsHub> hub) : Controller
    {


        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if(VotingSystem.VoteCounts.ContainsKey(type))
            {
                VotingSystem.VoteCounts[type]++;
                await hub.Clients.All.SendAsync("updateDeathlyHallowCount", 
                    VotingSystem.VoteCounts[VotingSystem.Cloak], 
                    VotingSystem.VoteCounts[VotingSystem.Stone], 
                    VotingSystem.VoteCounts[VotingSystem.Wand]);
            }
            
            return Accepted();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}