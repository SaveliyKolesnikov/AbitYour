using System;
using System.Timers;

namespace AbitYour.Models.NewDayTimer
{
    public class NewDayListener : INewDayEvent
    {
        #region SingletonDeclaration

        private static readonly Lazy<NewDayListener> Lazy =
            new Lazy<NewDayListener>(() => new NewDayListener());

        public static NewDayListener Instance => Lazy.Value;


        #endregion

        public event Action OnNewDay;
        private int _currentDate = DateTime.Now.Day;
        private readonly Timer _checkNewDayTimer;

        private NewDayListener()
        {
            var checkingDuration = TimeSpan.FromMinutes(30).TotalMilliseconds;

            _checkNewDayTimer = new Timer();
            _checkNewDayTimer.Interval = checkingDuration;
            _checkNewDayTimer.Elapsed += (sender, args) =>
            {
                if (_currentDate == DateTime.Now.Day) return;

                OnNewDay?.Invoke();
                _currentDate = DateTime.Now.Day;
            };
            _checkNewDayTimer.Start();
        }
    }
}
