using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NFTMarketPlace.WebApp.Base;
using NFTMarketPlace.WebApp.Models;

namespace NFTMarketPlace.WebApp.Pages.Shared;

public partial class ThemeWrapper : RazorComponentBase
{
    private bool Theme = false;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var theme = await GetTheme();
        Theme = theme is not null && theme.Name == "light";
    }

    async Task ChangeTheme(ChangeEventArgs e)
    {
        if (e is not null)
        {
            Theme = (bool)e.Value;

            var theme = new Theme
            {
                Name = Theme ? "light" : "dark"
            };

            await SetTheme(theme);
            await Js.InvokeVoidAsync("switchTheme", theme.Name);
        }
    }
}
