using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using AbitYour.Models.NewDayTimer;

namespace AbitYour.Hubs
{
    public class RequestHub : Hub
    {
        private static int _numOfRequests = 0;

        static RequestHub()
        {
            INewDayEvent newDay = NewDayListener.Instance;
            newDay.OnNewDay += () => _numOfRequests = 0;
        }

        public int GetNumberOfRequests() => _numOfRequests;

        public async Task AddRequest()
        {
            _numOfRequests++;
            await RefreshUsersCount();
        }

        private async Task RefreshUsersCount() =>
            await Clients.All.SendAsync("newRequest", _numOfRequests);
    }
}
