namespace MyToDo.Api.Services;

public class MemoService : IMemoService
{
    private readonly MyDbContext _dbContext;
    private readonly IMapper _mapper;

    public MemoService(MyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result> AddAsync(Memo model)
    {
        try
        {
            await _dbContext.Memos.AddAsync(model);
            var re = _dbContext.SaveChanges();
            if (re > 0)
            {
                return Result.Ok();
            }
            return Result.Fail("添加memo失败");
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
            var model = await _dbContext.Memos.SingleAsync(x => x.Id == id);
            _dbContext.Memos.Remove(model);
            var re = _dbContext.SaveChanges();
            if (re > 0)
            {
                return Result.Ok();
            }
            return Result.Fail("删除memo失败");
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
            var memos = await _dbContext.Memos
                .OrderByDescending(x => x.CreateDate)
                .ThenBy(x => x.Id)
                .Where(x => string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search))
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
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
            var memo = await _dbContext.Memos.SingleAsync(x => x.Id == id);

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
            var trackedMemo = await _dbContext.Memos.FindAsync(model.Id);
            if (trackedMemo != null)
            {
                _dbContext.Entry(trackedMemo).CurrentValues.SetValues(model);
                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine("更新Memo:");
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                Console.WriteLine("______________________________________");
                var re = await _dbContext.SaveChangesAsync();
                if (re > 0)
                {
                    return Result.Ok(_mapper.Map<MemoDto>(model));
                }
            }

            return Result.Fail("更新memo数据失败");
        }
        catch (Exception ex)
        {
            return Result.Fail("更新memo数据失败").WithError(ex.Message);
        }
    }
}