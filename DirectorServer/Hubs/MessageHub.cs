using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

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
        
        public Task SendCommandsToUser()
        {
            string message 
                = ClientInfo.getGroup(Context.ConnectionId).Equals("") 
                    ? ""
                    : CommandHolder.getEnabledCommands(ClientInfo.getGroup(Context.ConnectionId));
            return Clients.Caller.SendAsync("CommandUpdate", message);
        }
        
        public Task ClickedCommand(string command)
        {
            Console.WriteLine("user clicked command");
            CommandBuffer.addCommand(ClientInfo.getGroup(Context.ConnectionId),command);
            return Clients.Caller.SendAsync("ReturnClicked");
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
