using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class Student
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
