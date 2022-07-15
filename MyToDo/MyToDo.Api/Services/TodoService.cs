using System.Collections.ObjectModel;

namespace MyToDo.Api.Services;

public class TodoService : ITodoService
{
    private readonly MyDbContext _dbContext;
    private readonly IMapper _mapper;

    public TodoService(MyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result> AddAsync(ToDo model)
    {
        try
        {
            await _dbContext.ToDos.AddAsync(model);
            var re = _dbContext.SaveChanges();
            if (re > 0)
            {
                return Result.Ok();
            }
            return Result.Fail("添加todo数据失败");
        }
        catch (Exception ex)
        {
            return Result.Fail("添加todo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var todoFind = await _dbContext.ToDos.SingleAsync(x => x.Id == id);
            if (todoFind != null)
            {
                _dbContext.ToDos.Remove(todoFind);
                var re = _dbContext.SaveChanges();
                if (re > 0)
                {
                    return Result.Ok();
                }
                return Result.Fail("删除todo数据失败");
            }
            return Result.Fail("未查询到该条数据，删除todo数据失败");
        }
        catch (Exception ex)
        {
            return Result.Fail("删除todo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<List<ToDoDto>>> GetAllAsync(QueryParameter query)
    {
        try
        {
            var todos = await _dbContext.ToDos
                .OrderByDescending(x => x.CreateDate)
                .ThenBy(x => x.Id)
                .Where(x => string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search))
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return Result.Ok(_mapper.Map<List<ToDoDto>>(todos));
        }
        catch (Exception ex)
        {
            return Result.Fail("获取todos数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<List<ToDoDto>>> GetAllAsync(ToDoParameter query)
    {
        try
        {
            var todos = await _dbContext.ToDos
                .OrderByDescending(x => x.CreateDate)
                .ThenBy(x => x.Id)
                .Where(x => (string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search)) &&
                (query.Status == null || x.Status.Equals(query.Status)))
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return Result.Ok(_mapper.Map<List<ToDoDto>>(todos));
        }
        catch (Exception ex)
        {
            return Result.Fail("获取memo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<ToDoDto>> GetSingleAsync(int id)
    {
        try
        {
            var todo = await _dbContext.ToDos.SingleAsync(x => x.Id == id);
            return Result.Ok(_mapper.Map<ToDoDto>(todo));
        }
        catch (Exception ex)
        {
            return Result.Fail("获取todo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<SummaryDto>> Summary()
    {
        try
        {
            //待办事项结果
            var todosQuery = _dbContext.ToDos.OrderByDescending(x => x.CreateDate);

            //备忘录结果
            var memosQuery = _dbContext.Memos.OrderByDescending(x => x.CreateDate);
            SummaryDto summary = new()
            {
                Sum = todosQuery.Count(), //汇总待办事项数量
                CompletedCount = todosQuery.Where(t => t.Status == 1).Count() //统计完成数量
            };
            summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); //统计完成率
            summary.MemoeCount = memosQuery.Count();  //汇总备忘录数量
            var todos = await todosQuery.Where(t => t.Status == 0).ToListAsync();
            var memos = await memosQuery.ToListAsync();
            summary.ToDoList = new ObservableCollection<ToDoDto>(_mapper.Map<List<ToDoDto>>(todos));
            summary.MemoList = new ObservableCollection<MemoDto>(_mapper.Map<List<MemoDto>>(memos));

            return Result.Ok(summary);
        }
        catch (Exception ex)
        {
            return Result.Fail<SummaryDto>(ex.Message);
        }
    }

    public async Task<Result<ToDoDto>> UpdateAsync(ToDo model)
    {
        try
        {
            var trackedTodo = await _dbContext.ToDos.FindAsync(model.Id);
            if (trackedTodo != null)
            {
                _dbContext.Entry(trackedTodo).CurrentValues.SetValues(model);
                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine("更新Todo:");
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                Console.WriteLine("______________________________________");
                var re = await _dbContext.SaveChangesAsync();
                if (re > 0)
                {
                    return Result.Ok(_mapper.Map<ToDoDto>(model));
                }
            }

            return Result.Fail("更新todo数据失败");
        }
        catch (Exception ex)
        {
            return Result.Fail("更新todo数据失败").WithError(ex.Message);
        }
    }
}