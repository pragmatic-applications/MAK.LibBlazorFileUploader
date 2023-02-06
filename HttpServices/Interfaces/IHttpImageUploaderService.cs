namespace Interfaces;

public interface IHttpImageUploaderService
{
    List<ImageFile> ImageFiles
    {
        get;
        set;
    }
    bool IsDisabled
    {
        get;
        set;
    }
    string Message
    {
        get;
        set;
    }

    Task<string> UploadAsync(MultipartFormDataContent content, string baseUrl = null, string uploadEndpointUrl = null);
}
