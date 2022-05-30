namespace MyToDo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSqlsugarSetup(builder.Configuration);

        builder.Services.AddScoped<ITodoService, TodoService>();
        builder.Services.AddScoped<IMemoService, MemoService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        //添加AutoMapper
        var automapperConfog = new MapperConfiguration(config =>
        {
            config.CreateMap<ToDo, ToDoDto>().ReverseMap();
            config.CreateMap<Memo, MemoDto>().ReverseMap();
            config.CreateMap<User, UserDto>().ReverseMap();
        });

        builder.Services.AddSingleton(automapperConfog.CreateMapper());
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

public static class SqlsugarSetup
{
    public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration,
    string dbName = "ToDoCOnnections")
    {
        //多租户传List<ConnectionConfig>
        SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
        {
            DbType = SqlSugar.DbType.Sqlite,
            ConnectionString = configuration.GetConnectionString(dbName),
            IsAutoCloseConnection = true,
        },
         db => {  /***写AOP等方法***/});
        ISugarUnitOfWork<MyDbContext> context = new SugarUnitOfWork<MyDbContext>(sqlSugar);
        services.AddSingleton<ISugarUnitOfWork<MyDbContext>>(context);
    }
}