using Microsoft.AspNetCore.Components;
using Models.ViewModels;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class Materials
    {
        [Parameter]
        public int MaterialGroupId { get; set; }

        private MaterialsViewModel materialsView;
        private IEnumerable<MaterialViewModel> baseMaterialViewModels;
        private ICollection<MaterialViewModel> materialViewModelsTypeNotes = new List<MaterialViewModel>();
        private ICollection<MaterialViewModel> materialViewModelsTypeHomework = new List<MaterialViewModel>();
        private ICollection<MaterialViewModel> materialViewModelsTypeTest = new List<MaterialViewModel>();
        private bool isLoading = false;
        string roleString;

        protected override async Task OnParametersSetAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var role = user.FindFirst(ClaimTypes.Role);
            roleString = role.Value;
            StateHasChanged();
            await OnInitializedAsync();            
            await base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            materialViewModelsTypeNotes.Clear();
            materialViewModelsTypeHomework.Clear();
            materialViewModelsTypeTest.Clear();

            materialsView = await MaterialService.GetMaterialViewModelList(MaterialGroupId);
            baseMaterialViewModels = materialsView.MaterialViewModels;
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
            StateHasChanged();
        }

        private void NavigateToEditMaterial(int? id)
        {
            NavigationManager.NavigateTo("changeVisibility/" + id);
        }

        private void Delete(int? id)
        {
            //deleteMaterial
        }
    }
}
