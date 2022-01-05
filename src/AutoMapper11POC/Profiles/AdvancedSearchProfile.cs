using AutoMapper;
using AutoMapper11POC.Models;

namespace AutoMapper11POC.Profiles
{
    public class AdvancedSearchProfile : Profile
    {
        public AdvancedSearchProfile()
        {
            CreateMap<SavedItemSearchEntity, SavedItemSearchModel>();
        }
    }
}
