namespace MyToDo.Api.Services;

public interface ILoginService
{
    Task<Result<UserDto>> LoginAsync(string Account, string Password);

    Task<Result> Register(UserDto user);
}