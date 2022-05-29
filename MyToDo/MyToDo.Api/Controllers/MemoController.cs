using System.Text.Json;

namespace MyToDo.Api.Controllers;

/// <summary>
/// 备忘录控制器
/// </summary>
[ApiController]
[Route("api/memos")]
public class MemoController : ControllerBase
{
    private readonly IMemoService service;

    public MemoController(IMemoService service)
    {
        this.service = service;
    }

    [HttpGet("{memoId}", Name = nameof(GetMemo))]
    public async Task<ActionResult<MemoDto>> GetMemo(int memoId)
    {
        var ret = await service.GetSingleAsync(memoId);
        if (ret.IsSuccess)
        {
            return ret.Value != null ? Ok(ret.Value) : NotFound();
        }
        return BadRequest(ret.Errors.FirstOrDefault()?.Message);
    }

    [HttpGet(Name = nameof(GetMemos))]
    public async Task<ActionResult<List<MemoDto>>> GetMemos([FromQuery] QueryParameter param)
    {
        var ret = await service.GetAllAsync(param);
        if (ret.IsSuccess)
        {
            return ret.Value != null && ret.Value.Any() ? ret.Value : NotFound();
        }
        return BadRequest();
    }

    [HttpPost(Name = nameof(AddMemo))]
    public async Task<ActionResult> AddMemo([FromBody] Memo model)
    {
        var ret = await service.AddAsync(model);
        if (ret.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest(ret.Errors.FirstOrDefault()?.Message);
    }

    [HttpPut(Name = nameof(UpdateMemo))]
    public async Task<ActionResult<MemoDto>> UpdateMemo([FromBody] Memo model)
    {
        var ret = await service.UpdateAsync(model);
        if (ret.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest();
    }

    [HttpDelete("{memoId}", Name = nameof(DeleteMemo))]
    public async Task<ActionResult> DeleteMemo(int memoId)
    {
        var ret = await service.DeleteAsync(memoId);
        if (ret.IsSuccess)
        {
            return NoContent();
        }
        return BadRequest();
    }
}