using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Admin
{
    public class Company : IBaseEntityModel, IChangeTrackerEntity, IDeleteTrackerEntity
    {
        [Key]
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        public int DivisionId { get; set; }
        public string? NameEnglish { get; set; }
        public string? ShortEnglish { get; set; }
        public string? NameArabic { get; set; }
        public string? ShortArabic { get; set; }
        public byte[]? Logo { get; set; }
        public int RatingId { get; set; }
        public string? ContactPerson { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Address { get; set; }
        public string? Landmark { get; set; }
        public string? POBox { get; set; }
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }
}
