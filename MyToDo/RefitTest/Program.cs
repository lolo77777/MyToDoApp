using MyToDo.Share.Parameters;
using MyToDo.Share.Dtos;

using Refit;

namespace RefitTest;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var memoApi = RestService.For<IMemoApi>("https://localhost:5001/api");

        var reApiResGet = await memoApi.GetMemo(1);
        var reApiResGet2 = await memoApi.GetMemos(new QueryParameter { PageIndex = 0, PageSize = 10, Search = null });
        if (reApiResGet.IsSuccessStatusCode)
        {
        }
        var memodto = new MemoDto { Content = "refitAdd", Title = "refitAdd" };
        var reApiRes = await memoApi.AddMemo(memodto);
    }
}