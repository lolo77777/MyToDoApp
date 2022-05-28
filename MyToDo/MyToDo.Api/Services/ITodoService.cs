using FluentResults;

using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Services;

public interface ITodoService : IBaseService<ToDoDto, ToDo>
{
    Task<Result<List<ToDo>>> GetAllAsync(ToDoParameter query);

    Task<Result<SummaryDto>> Summary();
}