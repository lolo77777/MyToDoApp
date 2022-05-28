using FluentResults;

using MyToDo.Share.Parameters;

namespace MyToDo.Api.Services;

public interface IBaseService<TIn, TOut>
{
    Task<Result<List<TOut>>> GetAllAsync(QueryParameter query);

    Task<Result<TOut>> GetSingleAsync(int id);

    Task<Result> AddAsync(TIn model);

    Task<Result<TOut>> UpdateAsync(TIn model);

    Task<Result> DeleteAsync(int id);
}