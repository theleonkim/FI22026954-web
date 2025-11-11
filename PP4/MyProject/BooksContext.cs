using Microsoft.EntityFrameworkCore;

public class BooksContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TitleTag> TitlesTags => Set<TitleTag>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=data/books.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cambiar nombre de la tabla TitleTag -> TitlesTags
        modelBuilder.Entity<TitleTag>().ToTable("TitlesTags");

        // Orden de columnas en Title: TitleId, AuthorId, TitleName
        modelBuilder.Entity<Title>().Property(t => t.TitleId).HasColumnOrder(0);
        modelBuilder.Entity<Title>().Property(t => t.AuthorId).HasColumnOrder(1);
        modelBuilder.Entity<Title>().Property(t => t.TitleName).HasColumnOrder(2);
    }
}
