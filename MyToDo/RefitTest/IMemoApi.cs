using FluentResults;

using MyToDo.Api.Context;
using MyToDo.Share;

using Refit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefitTest
{
    public interface IMemoApi
    {
        [Get("/api/Memo/get?id={id}")]
        Task<ApiResult<Memo>> GetMemo([AliasAs("id")] int id);
    }
}