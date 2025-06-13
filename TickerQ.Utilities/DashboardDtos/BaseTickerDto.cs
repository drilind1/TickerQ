using System;

namespace TickerQ.Utilities.DashboardDtos
{
    internal class BaseTickerDto
    {
        public Guid Id { get; set; }
        public string Function { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}