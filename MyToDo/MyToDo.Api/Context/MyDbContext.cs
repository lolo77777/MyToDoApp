namespace MyToDo.Api.Context;

public class MyDbContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<Memo> Memos { get; set; }
    public DbSet<User> Users { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ToDo>()
        //    .ToTable("ToDo");
        //modelBuilder.Entity<Memo>()
        //    .ToTable("Memo");
        //modelBuilder.Entity<User>()
        //    .ToTable("User");
    }
}