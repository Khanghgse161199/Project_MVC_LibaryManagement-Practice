using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string CardId { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public bool IsAccept { get; set; }

    public bool IsActive { get; set; }

    public string CitizenId { get; set; } = null!;

    public string StudentCode { get; set; } = null!;

    public DateTime DatePay { get; set; }

    public bool IsPay { get; set; }

    public string PhoneNum { get; set; } = null!;

    public virtual Card Card { get; set; } = null!;
}
