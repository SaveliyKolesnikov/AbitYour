using Microsoft.AspNetCore.SignalR;
using System;
using AbitYour.Models.NewDayTimer;

namespace AbitYour.Hubs
{
    public class RequestHub : Hub
    {
        private static readonly Random NumOfRequestsStartValueGenerator = new Random(DateTime.Now.Millisecond);
        private static int _numOfRequests = 0;

        static RequestHub()
        {
            INewDayEvent newDay = NewDayListener.Instance;
            newDay.OnNewDay += () => _numOfRequests = 0;
        }

        public int GetNumberOfRequests() => _numOfRequests;

        public void AddRequest()
        {
            _numOfRequests++;
            Invoke();
        }

        private void Invoke()
        {
            Clients.All.SendAsync("newRequest", _numOfRequests);
        }
    }
}
