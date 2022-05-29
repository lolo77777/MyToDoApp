using FluentResults;

using Microsoft.AspNetCore.Mvc;

using MyToDo.Api.Context;
using MyToDo.Share;
using MyToDo.Shared.Dtos;

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
        [Get("/memos/{id}")]
        Task<IApiResponse<MemoDto>> GetMemo(int id);

        [Post("/memos")]
        Task<IApiResponse> AddMemo(MemoDto memo);
    }
}