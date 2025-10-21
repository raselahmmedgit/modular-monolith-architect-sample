namespace rapid.erp.ViewModel
{
    public class BaseViewMobel
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }

    public class BaseViewMobel<TModel> : BaseViewMobel
    {
        public TModel KeyValue { get; set; }
        public bool Success { get; set; }
        public bool HasRecord { get; set; }
        public string Message { get; set; }
    }
}
