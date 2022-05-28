using FreeSql.DataAnnotations;

namespace MyToDo.Api.Context;

public class BaseEntity
{
    [Column(IsIdentity = true)]
    public int Id { get; set; }

    [Column(ServerTime = DateTimeKind.Local)]
    public DateTime CreateDate { get; set; }

    [Column(ServerTime = DateTimeKind.Local)]
    public DateTime UpdateDate { get; set; }
}