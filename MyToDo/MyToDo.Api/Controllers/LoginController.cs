namespace MyToDo.Api.Controllers;

/// <summary>
/// 账户控制器
/// </summary>
[ApiController]
[Route("api/logins")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        this._service = service;
    }

    [HttpPost("/login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] UserDto param)
    {
        var ret = await _service.LoginAsync(param.Account, param.PassWord);
        if (ret.IsSuccess)
            return Ok(ret.Value);
        return BadRequest(ret.Errors.FirstOrDefault()?.Message);
    }

    [HttpPost("/register")]
    public async Task<ActionResult> Register([FromBody] UserDto param)
    {
        var ret = await _service.Register(param);
        if (ret.IsSuccess)
        {
            return Ok();
        }
        return BadRequest(ret.Errors.FirstOrDefault()?.Message);
    }
}