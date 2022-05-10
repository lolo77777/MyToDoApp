namespace MyToDo.MVVM.Common.Models;

public class MemoDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Status { get; set; }
}