using AutoMapper;

using FluentResults;

using FreeSql;

using MyToDo.Api.Context;
using MyToDo.Share.Parameters;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

using System.Collections.ObjectModel;

namespace MyToDo.Api.Services;

public class TodoService : ITodoService
{
    private readonly UnitOfWorkManager _unitOfWorkManager;
    private readonly IBaseRepository<ToDo, int> _toDoRepository;
    private readonly IBaseRepository<Memo, int> _memoRepository;
    private readonly IMapper _mapper;

    public TodoService(UnitOfWorkManager unitOfWorkManager, IBaseRepository<ToDo, int> todoRepository, IBaseRepository<Memo, int> memoRepository, IMapper mapper)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _toDoRepository = todoRepository;
        _memoRepository = memoRepository;
        _mapper = mapper;
    }

    public async Task<Result> AddAsync(ToDoDto model)
    {
        try
        {
            using IUnitOfWork unit = _unitOfWorkManager.Begin();
            var todo = _mapper.Map<ToDo>(model);
            await _toDoRepository.InsertAsync(todo);
            unit.Commit();
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
            using IUnitOfWork unit = _unitOfWorkManager.Begin();
            await _toDoRepository.DeleteAsync(id);
            unit.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail("添加todo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<List<ToDo>>> GetAllAsync(QueryParameter query)
    {
        using IUnitOfWork unit = _unitOfWorkManager.Begin();
        try
        {
            var todos = await _toDoRepository
                .Where(x => string.IsNullOrWhiteSpace(query.Search) ? true : x.Title.Contains(query.Search))
                .Page(query.PageIndex, query.PageSize)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
            unit.Commit();
            return Result.Ok(todos);
        }
        catch (Exception ex)
        {
            return Result.Fail("获取todos数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<List<ToDo>>> GetAllAsync(ToDoParameter query)
    {
        try
        {
            var todos = await _toDoRepository
                .Where(x => (string.IsNullOrWhiteSpace(query.Search) || x.Title.Contains(query.Search))
                && (query.Status == null || x.Status.Equals(query.Status)))
                .Page(query.PageIndex, query.PageSize)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();

            return Result.Ok(todos);
        }
        catch (Exception ex)
        {
            return Result.Fail("获取memo数据失败").WithError(ex.Message);
        }
    }

    public async Task<Result<ToDo>> GetSingleAsync(int id)
    {
        try
        {
            var todo = await _toDoRepository.GetAsync(id);

            return Result.Ok(todo);
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
            var todos = await _toDoRepository.Select.OrderByDescending(x => x.CreateDate).ToListAsync();
            //备忘录结果
            var memos = await _memoRepository.Select.OrderByDescending(x => x.CreateDate).ToListAsync();
            SummaryDto summary = new()
            {
                Sum = todos.Count, //汇总待办事项数量
                CompletedCount = todos.Where(t => t.Status == 1).Count() //统计完成数量
            };
            summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%"); //统计完成率
            summary.MemoeCount = memos.Count;  //汇总备忘录数量
            summary.ToDoList = new ObservableCollection<ToDoDto>(_mapper.Map<List<ToDoDto>>(todos.Where(t => t.Status == 0)));
            summary.MemoList = new ObservableCollection<MemoDto>(_mapper.Map<List<MemoDto>>(memos));
            return Result.Ok(summary);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<ToDo>> UpdateAsync(ToDoDto model)
    {
        using IUnitOfWork unit = _unitOfWorkManager.Begin();
        try
        {
            var dbTodo = _mapper.Map<ToDo>(model);
            var todo = await _toDoRepository.Where(x => x.Id.Equals(dbTodo.Id)).FirstAsync();
            todo.Title = dbTodo.Title;
            todo.Content = dbTodo.Content;
            todo.UpdateDate = DateTime.Now;
            todo.Status = dbTodo.Status;
            await _toDoRepository.UpdateAsync(todo);
            unit.Commit();
            return Result.Ok(todo);
        }
        catch (Exception ex)
        {
            return Result.Fail("更新memo数据失败").WithError(ex.Message);
        }
    }
}