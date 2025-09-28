namespace rapid.erp.ViewModel.Security
{
    public class ApplicationRoleViewModel : BaseViewMobel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public bool IsActive { get; set; }
    }
}
