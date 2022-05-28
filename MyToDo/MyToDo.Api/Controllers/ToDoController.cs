namespace MyToDo.Api.Controllers;

/// <summary>
/// 待办事项控制器
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class ToDoController : ControllerBase
{
    private readonly ITodoService service;

    public ToDoController(ITodoService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ApiResult<ToDo>> Get(int id) => await service.GetSingleAsync(id);

    [HttpGet]
    public async Task<ApiResult<List<ToDo>>> GetAll([FromQuery] ToDoParameter param) => await service.GetAllAsync(param);

    [HttpGet]
    public async Task<ApiResult<SummaryDto>> Summary() => await service.Summary();

    [HttpPost]
    public async Task<ApiResult> Add([FromBody] ToDoDto model) => await service.AddAsync(model);

    [HttpPost]
    public async Task<ApiResult<ToDo>> Update([FromBody] ToDoDto model) => await service.UpdateAsync(model);

    [HttpDelete]
    public async Task<ApiResult> Delete(int id) => await service.DeleteAsync(id);
}