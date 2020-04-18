using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DirectorServer;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using HubCallerContext = Microsoft.AspNet.SignalR.Hubs.HubCallerContext;
using IGroupManager = Microsoft.AspNet.SignalR.IGroupManager;

namespace DirectorServer.Hubs
{
    public class MessageHub : Hub
    {
        public Task SendDataToUser()
        {
            string message 
                = ClientInfo.getGroup(Context.ConnectionId).Equals("") 
                ? UnityClientList.getClientList()
                : UnityDataHolder.getData(ClientInfo.getGroup(Context.ConnectionId));
            return Clients.Caller.SendAsync("DataUpdate", message);
        }
        
        public Task JoinGroup(string group)
        {
            ClientInfo.setGroup(Context.ConnectionId, group);
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("UserConnected");
            await base.OnConnectedAsync();
            ClientInfo.addClient(Context.ConnectionId);
            
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            //await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            ClientInfo.removeClient(Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
        
        //SendMessageToAll      Clients.All.SendAsync("ReceiveMessage", message);
        //SendMessageToCaller   Clients.Caller.SendAsync("ReceiveMessage", message);
        //SendMessageToUser     Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        //SendMessageToGroup    Clients.Group(group).SendAsync("ReceiveMessage", message);
    }      
}
