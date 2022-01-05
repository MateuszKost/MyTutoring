namespace Models.ViewModels
{
    public class MaterialsViewModel
    {
        public IEnumerable<MaterialViewModel> MaterialViewModels { get; set; }
        public string? MaterialGroupName { get; set; }
    }
}
