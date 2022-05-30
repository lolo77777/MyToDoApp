using MyToDo.Share.Parameters;
using MyToDo.Shared.Dtos;

using Refit;

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