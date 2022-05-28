using FluentResults;

using MyToDo.Share.Parameters;

namespace MyToDo.Api.Services;

public interface IBaseService<TIn, TOut>
{
    Task<ApiResult<List<TOut>>> GetAllAsync(QueryParameter query);

    Task<ApiResult<TOut>> GetSingleAsync(int id);

    Task<ApiResult> AddAsync(TIn model);

    Task<ApiResult<TOut>> UpdateAsync(TIn model);

    Task<ApiResult> DeleteAsync(int id);
}