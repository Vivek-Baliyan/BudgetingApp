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
            .ForMember(dest => dest.AccountName, opts => opts.MapFrom(src => src.Account.AccountName));
        }
    }
}