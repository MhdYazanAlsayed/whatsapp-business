using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SprintBusiness.Shared.Helpers;
using SprintBusiness.Shared.Hubs.Enums;

namespace SprintBusiness.Shared.Hubs
{
    [Authorize]
    public class DefaultHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var employeeId = Context.User.Claims.Single(x => x.Type == "EmployeeId");

            SignalrManager.Add(HubType.Default ,int.Parse(employeeId.Value), Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var employeeId = Context.User.Claims.Single(x => x.Type == "EmployeeId");

            SignalrManager.Remove(HubType.Default , int.Parse(employeeId.Value), Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
