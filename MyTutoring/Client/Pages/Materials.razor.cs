using Microsoft.AspNetCore.Components;

namespace MyTutoring.Client.Pages
{
    public partial class Materials
    {
        [Parameter]
        public int MaterialGroupId { get; set; }

        protected override async Task OnInitializedAsync()
        {
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
