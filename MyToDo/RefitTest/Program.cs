using Refit;

using System.Text.Json;
using MyToDo.Api.Context;
using FluentResults;
using MyToDo.Share;

using Microsoft.AspNetCore.Mvc;
using MyToDo.Shared.Dtos;
using MyToDo.Share.Parameters;

namespace RefitTest;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var gitHubApi = RestService.For<IMemoApi>("https://localhost:5001/api");

        var reApiResGet = await gitHubApi.GetMemo(1);
        var reApiResGet2 = await gitHubApi.GetMemos(new QueryParameter { PageIndex = 0, PageSize = 10, Search = null });
        if (reApiResGet.IsSuccessStatusCode)
        {
        }
        var memodto = new MemoDto { Content = "refitAdd", Title = "refitAdd" };
        var reApiRes = await gitHubApi.AddMemo(memodto);
    }
}