namespace MyToDo.Api.Services;

public class MemoService : IMemoService
{
    private readonly UnitOfWorkManager _unitOfWorkManager;
    private readonly IBaseRepository<Memo, int> _memoRepository;
    private readonly IMapper _mapper;

    public MemoService(UnitOfWorkManager unitOfWorkManager, IBaseRepository<Memo, int> memoRepository, IMapper mapper)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _memoRepository = memoRepository;
        _mapper = mapper;
    }

    public async Task<Result> AddAsync(Memo model)
    {
        using IUnitOfWork unit = _unitOfWorkManager.Begin();
        try
        {
            await _memoRepository.InsertAsync(model);
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
        using IUnitOfWork unit = _unitOfWorkManager.Begin();
        try
        {
            await _memoRepository.DeleteAsync(id);
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
            var memos = await _memoRepository
                .Where(x => string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search))
                .Page(query.PageIndex, query.PageSize)
                .OrderByDescending(x => x.CreateDate)
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
            var memo = await _memoRepository.GetAsync(id);

            return Result.Ok(_mapper.Map<MemoDto>(memo));
        }
        catch (Exception ex)
        {
            return Result.Fail("获取memo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<MemoDto>> UpdateAsync(Memo model)
    {
        using IUnitOfWork unit = _unitOfWorkManager.Begin();
        try
        {
            var memo = await _memoRepository.Where(x => x.Id.Equals(model.Id)).FirstAsync();
            memo.Title = model.Title;
            memo.Content = model.Content;
            memo.UpdateDate = DateTime.Now;
            await _memoRepository.UpdateAsync(memo);
            unit.Commit();
            return Result.Ok(_mapper.Map<MemoDto>(memo));
        }
        catch (Exception ex)
        {
            return Result.Fail("更新memo数据失败").WithError(ex.Message);
        }
    }
}