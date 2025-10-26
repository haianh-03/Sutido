namespace Sutido.Model.Entites;

public partial class Point
{
    public long PointId { get; set; }

    public int TotalPoint { get; set; } = 0;

    public DateTimeOffset CreatedAt { get; set; }
    public long UserId { get; set; }

    public virtual ICollection<PointTransaction> PointTransactions { get; set; } = new List<PointTransaction>();

    public virtual User User { get; set; } = null!;
}
