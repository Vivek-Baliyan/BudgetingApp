using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<MasterCategory, MasterCategoryDto>();
            CreateMap<SubCategory, SubCategoryDto>();
            CreateMap<AccountTransaction, TransactionDto>()
            .ForMember(dest => dest.AccountName, opts => opts.MapFrom(src => src.Account.AccountName))
            .ForMember(dest => dest.SubCategoryName, opts => opts.MapFrom(src => src.SubCategory.CategoryName))
            .ForMember(dest => dest.MasterCategoryName, opts => opts.MapFrom(src => src.SubCategory.MasterCategory.CategoryName));
        }
    }
}