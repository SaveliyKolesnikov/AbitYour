using System;

namespace AbitYour.Models.NewDayTimer
{
    interface INewDayEvent
    {
        event Action OnNewDay;
    }
}
