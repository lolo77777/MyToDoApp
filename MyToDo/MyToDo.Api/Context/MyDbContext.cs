namespace MyToDo.Api.Context;

public class MyDbContext : SugarUnitOfWork
{
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<Memo> Memos { get; set; }
    public DbSet<User> Users { get; set; }
}

public class DbSet<T> : SimpleClient<T> where T : class, new()
{
}