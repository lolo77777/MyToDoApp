namespace MyToDo.Api.Controllers;

/// <summary>
/// 待办事项控制器
/// </summary>
[ApiController]
[Route("api/todos")]
public class ToDoController : ControllerBase
{
    private readonly ITodoService service;

    public ToDoController(ITodoService service)
    {
        this.service = service;
    }

    [HttpGet("{todoId}", Name = nameof(GetTodo))]
    public async Task<ActionResult<ToDoDto>> GetTodo(int todoId)
    {
        var ret = await service.GetSingleAsync(todoId);
        if (ret.IsSuccess)
        {
            return ret.Value != null ? ret.Value : NotFound();
        }
        return BadRequest();
    }

    [HttpGet(Name = nameof(GetTodos))]
    public async Task<ActionResult<List<ToDoDto>>> GetTodos([FromQuery] QueryParameter param)
    {
        var ret = await service.GetAllAsync(param);
        if (ret.IsSuccess)
        {
            return ret.Value != null && ret.Value.Any() ? ret.Value : NotFound();
        }
        return BadRequest();
    }

    [HttpGet("summary")]
    public async Task<ActionResult<SummaryDto>> GetSummary()
    {
        var ret = await service.Summary();
        if (ret.IsSuccess)
        {
            return ret.Value != null ? ret.Value : NotFound();
        }
        return BadRequest();
    }

    [HttpPost]
    public async Task<ActionResult> AddTodo([FromBody] ToDo model)
    {
        var ret = await service.AddAsync(model);
        if (ret.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest();
    }

    [HttpPut]
    public async Task<ActionResult<ToDoDto>> UpdateTodo([FromBody] ToDo model)
    {
        var ret = await service.UpdateAsync(model);
        if (ret.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest();
    }

    [HttpDelete("{todoId}")]
    public async Task<ActionResult> DeleteTodo(int todoId)
    {
        var ret = await service.DeleteAsync(todoId);
        if (ret.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest();
    }
}