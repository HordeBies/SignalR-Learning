using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.SampleMVC.Data;
using SignalR.SampleMVC.Hubs;
using SignalR.SampleMVC.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SignalR.SampleMVC.Controllers
{
    public class HomeController(IHubContext<DeathlyHallowsHub> voteHub, ApplicationDbContext context, IHubContext<OrderHub> orderHub) : Controller
    {
        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (VotingSystem.VoteCounts.ContainsKey(type))
            {
                VotingSystem.VoteCounts[type]++;
                await voteHub.Clients.All.SendAsync("updateDeathlyHallowCount",
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
        public IActionResult BasicChat()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Voting()
        {
            return View();
        }
        public IActionResult Houses()
        {
            return View();
        }
        [Authorize]
        public IActionResult Chat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatViewModel model = new()
            {
                UserId = userId,
                MaxRoomAllowed = 4,
                Rooms = context.ChatRooms.ToList()
            };
            return View(model);
        }

        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "Hasan", "Mehmet", "Ali", "Bies", "Bhrugen" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random rand = new();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new Order()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {

            context.Orders.Add(order);
            context.SaveChanges();
            await orderHub.Clients.All.SendAsync("NewOrderCreated");
            return RedirectToAction(nameof(Order));
        }
        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = context.Orders.ToList();
            return Json(new { data = productList });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}