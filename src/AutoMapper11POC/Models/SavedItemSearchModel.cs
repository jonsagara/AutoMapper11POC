namespace AutoMapper11POC.Models
{
    public class SavedItemSearchModel : IItemsAdvancedSearchModel
    {
        public string? Manufacturer { get; set; }
        public string? Importer { get; set; }
        public string? Model { get; set; }
        public string? Serial { get; set; }
    }
}
