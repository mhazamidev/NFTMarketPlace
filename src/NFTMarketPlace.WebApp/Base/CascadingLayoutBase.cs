using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace NFTMarketPlace.WebApp.Base;

public class CascadingLayoutBase : LayoutComponentBase
{

    [Inject]
    public IJSRuntime js { get; set; }
    [CascadingParameter]
    public MainLayoutCascadingValue LayoutValue { get; set; }
}
