namespace MyToDo.Api.Controllers;

/// <summary>
/// 账户控制器
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<Result<UserDto>> Login([FromBody] UserDto param) =>
        await _service.LoginAsync(param.Account, param.PassWord);

    [HttpPost]
    public async Task<Result> Resgiter([FromBody] UserDto param) =>
        await _service.Resgiter(param);
}