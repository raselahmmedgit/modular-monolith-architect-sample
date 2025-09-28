namespace rapid.erp.Core.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
