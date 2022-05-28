using Refit;

using System.Text.Json;
using MyToDo.Api.Context;
using FluentResults;
using MyToDo.Share;

namespace RefitTest;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var gitHubApi = RestService.For<IMemoApi>("https://localhost:7063");
        var memo = await gitHubApi.GetMemo(1);
    }
}