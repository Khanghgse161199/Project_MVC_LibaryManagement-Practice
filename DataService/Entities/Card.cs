using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class Card
{
    public string Id { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public bool IsActive { get; set; }

    public string StudentId { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Student Student { get; set; } = null!;
}
