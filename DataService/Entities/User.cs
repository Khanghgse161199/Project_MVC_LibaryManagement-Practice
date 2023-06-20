using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
