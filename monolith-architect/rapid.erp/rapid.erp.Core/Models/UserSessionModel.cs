namespace rapid.erp.Core.Models
{
    public class UserSessionModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string PreviousData { get; set; }
    }
}
