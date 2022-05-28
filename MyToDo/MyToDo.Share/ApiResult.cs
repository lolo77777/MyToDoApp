namespace MyToDo.Share;

public class ApiResult<T>
{
    public bool IsSuccess { get; set; }
    public List<string> Successes { get; set; }
    public List<string> Errors { get; set; }
    public T Result { get; set; }
}

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public List<string> Successes { get; set; }
    public List<string> Errors { get; set; }
}

public static class ApiResultEx
{
    public static ApiResult ToApiResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new ApiResult
            {
                IsSuccess = result.IsSuccess,
                Successes = result.Successes.Select(t => t.Message).ToList(),
            };
        }
        else
        {
            return new ApiResult
            {
                IsSuccess = result.IsSuccess,
                Errors = result.Errors.Select(t => t.Message).ToList(),
            };
        }
    }

    public static ApiResult<T> ToApiResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new ApiResult<T>
            {
                IsSuccess = result.IsSuccess,
                Successes = result.Successes.Select(t => t.Message).ToList(),
                Result = result.ValueOrDefault
            };
        }
        else
        {
            return new ApiResult<T>
            {
                IsSuccess = result.IsSuccess,
                Errors = result.Errors.Select(t => t.Message).ToList(),
                Result = result.ValueOrDefault
            };
        }
    }
}