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
        builder.Services.AddDbContext<MyDbContext>(option =>
        option.UseSqlite(builder.Configuration.GetConnectionString("ToDoCOnnections")));

        builder.Services.AddScoped<ITodoService, TodoService>();
        builder.Services.AddScoped<IMemoService, MemoService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        //Ìí¼ÓAutoMapper
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