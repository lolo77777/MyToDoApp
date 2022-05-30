namespace MyToDo.Api.Services;

public class MemoService : IMemoService
{
    private readonly ISugarUnitOfWork<MyDbContext> _sugarUnit;
    private readonly IMapper _mapper;

    public MemoService(ISugarUnitOfWork<MyDbContext> sugarUnit, IMapper mapper)
    {
        _sugarUnit = sugarUnit;
        _mapper = mapper;
    }

    public async Task<Result> AddAsync(Memo model)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            await unit.Memos.InsertAsync(model);
            unit.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            await unit.Memos.DeleteByIdAsync(id);
            unit.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<List<MemoDto>>> GetAllAsync(QueryParameter query)
    {
        try
        {
            var unit = _sugarUnit.CreateContext();
            var memos = await unit.Memos.GetPageListAsync(
                x => string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search),
                new PageModel { PageIndex = query.PageIndex, PageSize = query.PageSize },
                x => x.CreateDate, OrderByType.Desc);
            unit.Commit();
            return Result.Ok(_mapper.Map<List<MemoDto>>(memos));
        }
        catch (Exception ex)
        {
            return Result.Fail("获取memo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<MemoDto>> GetSingleAsync(int id)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            var memo = await unit.Memos.GetByIdAsync(id);
            unit.Commit();
            return Result.Ok(_mapper.Map<MemoDto>(memo));
        }
        catch (Exception ex)
        {
            return Result.Fail("获取memo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<MemoDto>> UpdateAsync(Memo model)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            var memo = await unit.Memos.GetFirstAsync(x => x.Id.Equals(model.Id));
            memo.Title = model.Title;
            memo.Content = model.Content;
            memo.UpdateDate = DateTime.Now;
            await unit.Memos.UpdateAsync(memo);
            unit.Commit();
            return Result.Ok(_mapper.Map<MemoDto>(memo));
        }
        catch (Exception ex)
        {
            return Result.Fail("更新memo数据失败").WithError(ex.Message);
        }
    }
}