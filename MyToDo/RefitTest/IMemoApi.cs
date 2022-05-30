using FluentResults;

using Microsoft.AspNetCore.Mvc;

using MyToDo.Api.Context;
using MyToDo.Share;
using MyToDo.Share.Parameters;
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

        [Get("/memos")]
        Task<IApiResponse<List<MemoDto>>> GetMemos(QueryParameter queryParameter);

        [Post("/memos")]
        Task<IApiResponse> AddMemo(MemoDto memo);
    }
}