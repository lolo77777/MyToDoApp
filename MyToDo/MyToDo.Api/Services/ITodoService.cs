namespace MyToDo.Api.Services;

public interface ITodoService : IBaseService<ToDoDto, ToDo>
{
    Task<ApiResult<List<ToDo>>> GetAllAsync(ToDoParameter query);

    Task<ApiResult<SummaryDto>> Summary();
}