namespace MyToDo.MVVM.Services;

public interface IToDoApi
{
    [Get("/todos/{id}")]
    Task<IApiResponse<ToDoDto>> GetTodo(int id);

    [Get("/todos")]
    Task<IApiResponse<List<ToDoDto>>> GetTodos(ToDoParameter queryParameter);

    [Post("/todos")]
    Task<IApiResponse> AddTodo(ToDoDto todo);

    [Delete("/todos/{id}")]
    Task<IApiResponse> DeleteTodo(int id);

    [Put("/todos")]
    Task<IApiResponse<ToDoDto>> UpdateTodo(ToDoDto todo);
}