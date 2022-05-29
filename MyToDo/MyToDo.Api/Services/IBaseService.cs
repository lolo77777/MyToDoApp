namespace MyToDo.Api.Services;

public interface IBaseService<T, TAdd, TUpdate>
{
    Task<Result<List<T>>> GetAllAsync(QueryParameter query);

    Task<Result<T>> GetSingleAsync(int id);

    Task<Result> AddAsync(TAdd model);

    Task<Result<T>> UpdateAsync(TUpdate model);

    Task<Result> DeleteAsync(int id);
}