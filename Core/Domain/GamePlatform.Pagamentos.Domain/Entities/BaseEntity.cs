namespace GamePlatform.Pagamentos.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; internal set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}