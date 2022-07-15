namespace MyToDo.Api.Context;

public class BaseEntity
{
    //[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }
}