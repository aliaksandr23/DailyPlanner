﻿namespace DailyPlanner.Domain.Entities
{
    public sealed class Column : BaseAuditableEntity
    {
        public string Title { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}