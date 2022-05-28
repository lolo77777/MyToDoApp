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
    public async Task<Result<Memo>> Get(int id) => await service.GetSingleAsync(id);

    [HttpGet]
    public async Task<Result<List<Memo>>> GetAll([FromQuery] QueryParameter param) => await service.GetAllAsync(param);

    [HttpPost]
    public async Task<Result> Add([FromBody] MemoDto model) => await service.AddAsync(model);

    [HttpPost]
    public async Task<Result<Memo>> Update([FromBody] MemoDto model) => await service.UpdateAsync(model);

    [HttpDelete]
    public async Task<Result> Delete(int id) => await service.DeleteAsync(id);
}