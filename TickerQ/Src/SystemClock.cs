﻿using System;
using TickerQ.Utilities.Interfaces;

namespace TickerQ
{
    public class TickerSystemClock : ITickerClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
