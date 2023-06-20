using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class History
{
    public string Id { get; set; } = null!;

    public string CardId { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int Number { get; set; }

    public string BookId { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public virtual Card Card { get; set; } = null!;
}
