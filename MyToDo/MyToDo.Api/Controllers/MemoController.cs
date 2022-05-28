namespace MyToDo.Api.Controllers;

/// <summary>
/// 备忘录控制器
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class MemoController : ControllerBase
{
    private readonly IMemoService service;

    public MemoController(IMemoService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ApiResult<Memo>> Get(int id) => await service.GetSingleAsync(id);

    [HttpGet]
    public async Task<ApiResult<List<Memo>>> GetAll([FromQuery] QueryParameter param) => await service.GetAllAsync(param);

    [HttpPost]
    public async Task<ApiResult> Add([FromBody] MemoDto model) => await service.AddAsync(model);

    [HttpPost]
    public async Task<ApiResult<Memo>> Update([FromBody] MemoDto model) => await service.UpdateAsync(model);

    [HttpDelete]
    public async Task<ApiResult> Delete(int id) => await service.DeleteAsync(id);
}