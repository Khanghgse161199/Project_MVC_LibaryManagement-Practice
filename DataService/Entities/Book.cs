using System;
using System.Collections.Generic;

namespace DataService.Entities;

public partial class Book
{
    public string Id { get; set; } = null!;

    public string Creator { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime PulishDate { get; set; }

    public string Pulisher { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User CreatorNavigation { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
