using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Author
{
    [Key] public int AuthorId { get; set; }
    [Required] public string AuthorName { get; set; } = null!;
    public ICollection<Title> Titles { get; set; } = new List<Title>();
}

public class Title
{
    [Key] public int TitleId { get; set; }
    [Required] public string TitleName { get; set; } = null!;
    [Required] public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public ICollection<TitleTag> TitleTags { get; set; } = new List<TitleTag>();
}

public class Tag
{
    [Key] public int TagId { get; set; }
    [Required] public string TagName { get; set; } = null!;
    public ICollection<TitleTag> TitleTags { get; set; } = new List<TitleTag>();
}

public class TitleTag
{
    [Key] public int TitleTagId { get; set; }
    [Required] public int TitleId { get; set; }
    public Title Title { get; set; } = null!;
    [Required] public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}
