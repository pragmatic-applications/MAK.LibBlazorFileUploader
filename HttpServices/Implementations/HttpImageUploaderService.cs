namespace HttpServices;

public class HttpImageUploaderService : IHttpImageUploaderService
{
    public HttpImageUploaderService(HttpClient httpClient) => this.HttpClient = httpClient;

    protected HttpClient HttpClient
    {
        get; set;
    }

    protected string Url
    {
        get; set;
    }

    public List<ImageFile> ImageFiles { get; set; } = new();
    public string Message { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = true;

    public async Task<string> UploadAsync(MultipartFormDataContent content, string baseUrl = null, string uploadEndpointUrl = null)
    {
        var postResult = await this.HttpClient.PostAsync(uploadEndpointUrl, content);
        var postContent = await postResult.Content.ReadAsStringAsync();

        return !postResult.IsSuccessStatusCode ? throw new ApplicationException(postContent) : Path.Combine(baseUrl, postContent);
    }
}
