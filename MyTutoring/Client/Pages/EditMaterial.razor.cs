using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Models.Models;
using Models.ViewModels;
using Services.FileConverter;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class EditMaterial
    {
        [Parameter]
        public int MaterialId { get; set; }

        public MaterialViewModel model = new MaterialViewModel();
        private IEnumerable<MaterialTypeSingleViewModel> materialTypeSingleViewModels;
        private IEnumerable<MaterialGroupSingleViewModel> materialGroupSingleViewModels;
        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }

        public int SelectedMaterialTypeId { get; set; }
        public int SelectedMaterialGroupId { get; set; }
        public string Description { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.FindFirst("id");
            var role = user.FindFirst(ClaimTypes.Role);

            model = await MaterialService.GetMaterial(new SingleItemByIdRequest { Id = MaterialId });

            SelectedMaterialTypeId = model.MaterialTypeId;
            SelectedMaterialGroupId = model.MaterialGroupId;
            materialGroupSingleViewModels = await MaterialsGroupService.GetMaterialGroupList(new UserInfo() { Id = userId.Value, Role = role.Value });
            materialTypeSingleViewModels = await MaterialsTypeService.GetMaterialTypeList();
        }

        private void OnSelectType(ChangeEventArgs e)
        {
            SelectedMaterialTypeId = int.Parse(e.Value.ToString());
            Console.WriteLine(SelectedMaterialTypeId);
        }

        private void OnSelectGroup(ChangeEventArgs e)
        {
            SelectedMaterialGroupId = int.Parse(e.Value.ToString());
            Console.WriteLine(SelectedMaterialGroupId);
        }

        private async void UploadFiles()
        {
            ShowErrors = false;

            if (SelectedMaterialTypeId == 0 || SelectedMaterialGroupId == 0)
            {
                Error = "Wybierz typ i grupe materiału";
                ShowErrors = true;
            }
            if (model.Description == null || model.Description.Length < 5)
            {
                Error = "Pole opisu jest wymagane";
                ShowErrors = true;
            }
                        
            model.MaterialTypeId = SelectedMaterialTypeId;
            model.MaterialGroupId = SelectedMaterialGroupId;
            model.FileName = "edit";
            model.Data = "edit";

            var result = await MaterialService.EditMaterial(model);

            if (result.Successful)
            {
                ShowErrors = false;
                Success = true;
                StateHasChanged();

                NavigationManager.NavigateTo("/");
                //Task.Delay(2000);

                //NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Message;
                ShowErrors = true;
                StateHasChanged();
            }
        }

        private void ClearAll()
        {
            model = new MaterialViewModel();
            //NavigationManager.NavigateTo("/");
        }
    }
}
