namespace rapid.erp.EntityModel
{
    public interface IBaseEntityModel
    {
        bool IsActive { get; set; }
    }

    public class BaseEntityModel : IBaseEntityModel
    {
        public bool IsActive { get; set; }
    }

    public interface IChangeTrackerEntity
    {
        DateTime CreatedDate { get; set; }
        string? CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string? UpdatedBy { get; set; }
    }

    public class ChangeTrackerEntity : IChangeTrackerEntity
    {
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class DeleteTrackerEntity : IDeleteTrackerEntity
    {
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }

    public interface IDeleteTrackerEntity
    {
        bool? IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
        string? DeletedBy { get; set; }
    }

}
