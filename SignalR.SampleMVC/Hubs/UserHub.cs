﻿using Microsoft.AspNetCore.SignalR;

namespace SignalR.SampleMVC.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
        }
        public static int TotalUsers { get; set; } = 0;
        public override async Task OnConnectedAsync()
        {
            TotalUsers++;
            await Clients.All.SendAsync("UpdateTotalUsers", TotalUsers);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            await Clients.All.SendAsync("UpdateTotalUsers", TotalUsers);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
