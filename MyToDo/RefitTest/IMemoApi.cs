using MyToDo.Share.Dtos;
using MyToDo.Share.Parameters;

using Refit;

namespace RefitTest;

public interface IMemoApi
{
    [Get("/memos/{id}")]
    Task<IApiResponse<MemoDto>> GetMemo(int id);

    [Get("/memos")]
    Task<IApiResponse<List<MemoDto>>> GetMemos(QueryParameter queryParameter);

    [Post("/memos")]
    Task<IApiResponse> AddMemo(MemoDto memo);

    [Delete("/memos/{id}")]
    Task<IApiResponse> DeleteMemo(int id);

    [Put("/memos")]
    Task<IApiResponse<MemoDto>> UpdateMemo(MemoDto memo);
}