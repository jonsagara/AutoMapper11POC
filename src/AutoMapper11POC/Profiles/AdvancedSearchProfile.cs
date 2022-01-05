using AutoMapper;
using AutoMapper11POC.Models;

namespace AutoMapper11POC.Profiles
{
    public class AdvancedSearchProfile : Profile
    {
        public AdvancedSearchProfile()
        {
            CreateMap<SavedItemSearchEntity, SavedItemSearchModel>();

            //CreateMap<SavedItemSearchEntity, AbstractItemsAdvancedSearchModel>();

            //CreateMap<SavedItemSearchEntity, ConcreteItemsAdvancedSearchModel>()
            //    .IncludeBase<SavedItemSearchEntity, AbstractItemsAdvancedSearchModel>();

            //CreateMap<SavedItemSearchEntity, AnotherConcreateItemsAdvancedSearchModel>()
            //    .IncludeBase<SavedItemSearchEntity, AbstractItemsAdvancedSearchModel>();

            //CreateMap<SavedItemSearchEntity, UnrelatedSearchModel>();
        }
    }
}
