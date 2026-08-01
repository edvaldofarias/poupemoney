namespace PoupeMoney.Core.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime Created { get; private set; } = DateTime.UtcNow;

    public DateTime? Updated { get; private set; } = null;

    public bool Deleted { get; private set; } = false;

    protected void Delete()
    {
        Deleted = true;
        Updated = DateTime.UtcNow;
    }

    protected void Restore()
    {
        Deleted = false;
        Updated = DateTime.UtcNow;
    }

    protected abstract void Validate();

    protected void Update() => Updated = DateTime.UtcNow;
}