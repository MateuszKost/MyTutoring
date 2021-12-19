using Microsoft.AspNetCore.Components;
using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class Materials
    {
        [Parameter]
        public int MaterialGroupId { get; set; }

        private IEnumerable<MaterialViewModel> materialViewModels;
        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            materialViewModels = await MaterialService.GetMaterialViewModelList(MaterialGroupId);
            isLoading = true; 
            //image = await ImageService.GetImage(ImageId);
            //if (image.Tags?.Any() == true)
            //    _tags = string.Join(" ", image.Tags.Select(x => "#" + x));
            //if (DateTime.TryParse(image.Date, out var parsed))
            //{
            //    _date = parsed;
            //}
            //else
            //{
            //    _date = DateTime.Now;
            //}
        }
    }
}
