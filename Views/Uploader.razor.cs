namespace Views;

public partial class Uploader
{
    [Inject]
    public IHttpImageUploaderService HttpImageUploaderService
    {
        get; set;
    }

    public string ImgUrl
    {
        get; set;
    }

    public List<string> Images { get; set; } = new();

    [Parameter]
    public EventCallback<string> OnChange
    {
        get; set;
    }

    [Parameter]
    public string AcceptTypeList
    {
        get; set;
    }

    [Parameter]
    public string ToImageType
    {
        get; set;
    }

    [Parameter]
    public string Css
    {
        get; set;
    }

    [Parameter]
    public bool Multiple
    {
        get; set;
    }

    [Parameter]
    public string BaseUrl
    {
        get; set;
    }

    [Parameter]
    public string UploadEndpointUrl
    {
        get; set;
    }

    //Uploader.MIME_TYPES
    public const string MIME_TYPES = "image/png, image/jpeg, image/gif";
    public const string CSS = "btn btn_custom_01 width_100_pc margin_bottom_5 pad_top_bottom_1 file_input";

    private async Task HandleSelected(InputFileChangeEventArgs e)
    {
        var imageFiles = e.GetMultipleFiles();

        foreach(var imageFile in imageFiles)
        {
            if(imageFile != null)
            {
                var resizedFile = await imageFile.RequestImageFileAsync($"image/{this.ToImageType}", 300, 500);

                using var stream = resizedFile.OpenReadStream(resizedFile.Size);
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(stream, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);

                this.ImgUrl = await this.HttpImageUploaderService.UploadAsync(content, this.BaseUrl, this.UploadEndpointUrl);
                await this.OnChange.InvokeAsync(this.ImgUrl);

                if(this.Multiple)
                {
                    this.Images.Add(this.ImgUrl);
                }
            }
        }
    }
}

//public class UIData
//{
//    public const string AcceptTypeList = "image/png, image/jpeg, image/gif";
//}
