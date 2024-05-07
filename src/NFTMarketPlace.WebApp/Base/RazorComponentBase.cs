using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using NFTMarketPlace.WebApp.Models;

namespace NFTMarketPlace.WebApp.Base;

public class RazorComponentBase : ComponentBase
{
    [CascadingParameter]
    public MainLayoutCascadingValue LayoutValue { get; set; }
    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }
    [Inject] protected IJSRuntime Js { get; set; }
    [Inject] protected NavigationManager Navigator { get; set; }
    [Inject] private ILocalStorageService _localStorage { get; set; }
    private const string ThemeLocalStorageKey = "theme";


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var theme = await GetTheme();
            if (theme is not null)
            {
                await Js.InvokeVoidAsync("switchTheme", theme.Name);
            }
        }
    }

    public async Task SetTheme(Theme theme)
        => await _localStorage.SetItemAsync(ThemeLocalStorageKey, theme);

    public async Task<Theme?> GetTheme()
        => await _localStorage.GetItemAsync<Theme>(ThemeLocalStorageKey);

    public void ShowMessage(string message, MessageType type = MessageType.Success)
    {
        LayoutValue.ShowMessage(message, type);
    }
    protected UserSession? CurrentUser
    {
        get
        {
            return GetCurrentUser().GetAwaiter().GetResult();
        }
    }
    private async Task<UserSession?> GetCurrentUser()
    {
        if (AuthenticationState is not null)
        {
            var authState = await AuthenticationState;
            var user = authState?.User;
            if (user is not null && user.Identity.IsAuthenticated)
            {
                var id = user.Claims.First(x => x.Type == "Id")?.Value;
                var username = user.Claims.FirstOrDefault(claim => claim.Type == "username")?.Value;
                return new UserSession
                {
                    UserId = id,
                    Username = username
                };
            }
        }
        return null;
    }
    //public async Task<FileTemp?> GetFile(InputFileChangeEventArgs e)
    //{
    //    try
    //    {
    //        if (e.File != null)
    //        {
    //            var buffer = new byte[e.File.Size];
    //            var size = e.File.Size / 1024;
    //            var fileContent = new StreamContent(e.File.OpenReadStream(1024 * 1024 * 15));
    //            buffer = await fileContent.ReadAsByteArrayAsync();
    //            var result = new FileTemp(
    //                Path.GetFileNameWithoutExtension(e.File.Name),
    //                buffer,
    //                (byte)size,
    //                e.File.ContentType,
    //                Path.GetExtension(e.File.Name));
    //            return result;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, MessageType.Error);
    //    }
    //    return null;
    //}

    //public async Task<string> GetFileUrl(Guid id)
    //{
    //    var query = await Mediator.Send(new GetFileByIdQuery(id));
    //    if (query.ValidationResult.IsValid)
    //        return $"data:{query.Result.ContentType};base64,{Convert.ToBase64String(query.Result.Content)}";
    //    return string.Empty;
    //}


    public PageStatus PageStatus { get; set; }


    //protected UserSession? CurrentUser
    //{
    //    get
    //    {
    //        return GetCurrentUser().GetAwaiter().GetResult();
    //    }
    //}
    //private async Task<UserSession?> GetCurrentUser()
    //{
    //    if (AuthenticationState is not null)
    //    {
    //        var authState = await AuthenticationState;
    //        var user = authState?.User;
    //        if (user is not null && user.Identity.IsAuthenticated)
    //        {
    //            var id = user.Claims.First(x => x.Type == "Id").Value;
    //            return new UserSession
    //            {
    //                UserId = id.ToGuid(),
    //                Email = user.Claims.First(x => x.Type == ClaimTypes.Email).Value,
    //                Name = user.Claims.First(x => x.Type == ClaimTypes.Name).Value,
    //                Username = user.Claims.First(x => x.Type == "Username").Value
    //            };
    //        }
    //    }
    //    return null;
    //}




}

public enum MessageType
{
    Error,
    Warning,
    Success,
    Info
}
public enum PageStatus
{
    List,
    Update,
    Create,
    Delete,
    ViewItem,
    CSBINS,
    History
}