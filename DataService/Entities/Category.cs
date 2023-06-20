using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
