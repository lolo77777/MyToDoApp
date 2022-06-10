using MemoDto = MyToDo.Share.Dtos.MemoDto;

namespace MyToDo.MVVM.Services
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