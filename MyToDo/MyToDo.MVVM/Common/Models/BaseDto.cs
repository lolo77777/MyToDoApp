namespace MyToDo.MVVM.Common.Models;

public class BaseDto
{
    public int Id { get; set; }
    public DateTime CreatDateTime { get; set; } = DateTime.Now;
    public DateTime UpdateDateTime { get; set; } = DateTime.Now;
}