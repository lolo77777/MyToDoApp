using System.Collections.ObjectModel;

namespace MyToDo.Api.Services;

public class TodoService : ITodoService
{
    private readonly ISugarUnitOfWork<MyDbContext> _sugarUnit;
    private readonly IMapper _mapper;

    public TodoService(ISugarUnitOfWork<MyDbContext> sugarUnit, IMapper mapper)
    {
        _sugarUnit = sugarUnit;
        _mapper = mapper;
    }

    public async Task<Result> AddAsync(ToDo model)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            await unit.ToDos.InsertAsync(model);
            var re = unit.Commit();
            if (!re)
            {
                return Result.Fail("添加todo数据失败");
            }
            return Result.Ok();
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
            using var unit = _sugarUnit.CreateContext();
            await unit.ToDos.DeleteByIdAsync(id);
            unit.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail("添加todo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<List<ToDoDto>>> GetAllAsync(QueryParameter query)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            var todos = await unit.ToDos.GetPageListAsync(
                x => string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search),
              new PageModel { PageIndex = query.PageIndex, PageSize = query.PageSize },
              x => x.CreateDate, OrderByType.Desc);
            unit.Commit();
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
            using var unit = _sugarUnit.CreateContext();
            var todos = await unit.ToDos.GetPageListAsync(
                x => (string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search)) &&
                (query.Status == null || x.Status.Equals(query.Status)),
              new PageModel { PageIndex = query.PageIndex, PageSize = query.PageSize },
              x => x.CreateDate, OrderByType.Desc);
            unit.Commit();
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
            using var unit = _sugarUnit.CreateContext();
            var todo = await unit.ToDos.GetByIdAsync(id);
            unit.Commit();
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
            using var unit = _sugarUnit.CreateContext();
            //待办事项结果
            var todos = (await unit.ToDos.GetListAsync()).OrderByDescending(x => x.CreateDate).ToList();

            //备忘录结果
            var memos = (await unit.Memos.GetListAsync()).OrderByDescending(x => x.CreateDate).ToList();
            SummaryDto summary = new()
            {
                Sum = todos.Count, //汇总待办事项数量
                CompletedCount = todos.Where(t => t.Status == 1).Count() //统计完成数量
            };
            summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); //统计完成率
            summary.MemoeCount = memos.Count;  //汇总备忘录数量
            summary.ToDoList = new ObservableCollection<ToDoDto>(_mapper.Map<List<ToDoDto>>(todos.Where(t => t.Status == 0)));
            summary.MemoList = new ObservableCollection<MemoDto>(_mapper.Map<List<MemoDto>>(memos));
            unit.Commit();
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
            using var unit = _sugarUnit.CreateContext();
            var todo = await unit.ToDos.GetFirstAsync(x => x.Id.Equals(model.Id));
            todo.Title = model.Title;
            todo.Content = model.Content;
            todo.UpdateDate = DateTime.Now;
            todo.Status = model.Status;
            await unit.ToDos.UpdateAsync(todo);
            unit.Commit();
            return Result.Ok(_mapper.Map<ToDoDto>(todo));
        }
        catch (Exception ex)
        {
            return Result.Fail("更新memo数据失败").WithError(ex.Message);
        }
    }
}