using AutoMapper;
using rapid.erp.Core.Security;
using rapid.erp.EntityModel.Admin;
using rapid.erp.ViewModel.Admin;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.Core.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ApplicationUserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationRoleViewModel, ApplicationRole>().ReverseMap();
            CreateMap<ApplicationUserRoleViewModel, ApplicationUserRole>().ReverseMap();

            CreateMap<CompanyViewModel, Company>().ReverseMap();
            CreateMap<CompanyCreateViewModel, Company>().ReverseMap();
            CreateMap<CompanyEditViewModel, Company>().ReverseMap();
        }
    }
}
