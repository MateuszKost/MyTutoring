using Microsoft.AspNetCore.Components;
using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class Materials
    {
        [Parameter]
        public int MaterialGroupId { get; set; }

        private IEnumerable<MaterialViewModel> baseMaterialViewModels;
        private ICollection<MaterialViewModel> materialViewModelsTypeNotes = new List<MaterialViewModel>();
        private ICollection<MaterialViewModel> materialViewModelsTypeHomework = new List<MaterialViewModel>();
        private ICollection<MaterialViewModel> materialViewModelsTypeTest = new List<MaterialViewModel>();
        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            baseMaterialViewModels = await MaterialService.GetMaterialViewModelList(MaterialGroupId);
            foreach (var materialViewModel in baseMaterialViewModels)
            {
                if(materialViewModel.MaterialTypeId == 1)
                {
                    materialViewModelsTypeNotes.Add(materialViewModel);
                }
                if (materialViewModel.MaterialTypeId == 2)
                {
                    materialViewModelsTypeHomework.Add(materialViewModel);
                }
                if (materialViewModel.MaterialTypeId == 3)
                {
                    materialViewModelsTypeTest.Add(materialViewModel);
                }
            }
            isLoading = true; 
        }
    }
}
