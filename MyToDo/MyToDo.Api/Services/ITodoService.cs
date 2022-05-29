namespace MyToDo.Api.Services;

public interface ITodoService : IBaseService<ToDoDto, ToDo, ToDo>
{
    Task<Result<List<ToDoDto>>> GetAllAsync(ToDoParameter query);

    Task<Result<SummaryDto>> Summary();
}