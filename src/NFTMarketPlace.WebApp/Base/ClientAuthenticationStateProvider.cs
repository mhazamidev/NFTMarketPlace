using Blazored.LocalStorage;
using Mhazami.Utility;
using Microsoft.AspNetCore.Components.Authorization;
using NFTMarketPlace.WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NFTMarketPlace.Common.Utility;

namespace NFTMarketPlace.WebApp.Base;


public class ClientAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    private string TokenKey = "userToken";
    private AuthenticationState Anonymous => new(new(new ClaimsIdentity()));

    public ClientAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;

    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var savedToken = await _localStorage.GetItemAsync<TokenVM>(TokenKey);
            if (savedToken == null)
                return Anonymous;
            return BuildAuthenticatedState(savedToken);

        }
        catch
        {
            return new(new(new ClaimsIdentity()));
        }
    }

    public async Task<bool> MarkUserAsAuthenticated(TokenVM tokenVm, bool writeToStorage = true)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("bearer", tokenVm.Token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(tokenVm.Token), "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
            if (writeToStorage)
                await _localStorage.SetItemAsync(TokenKey, tokenVm);
            return true;
        }
        catch
        {
            return false;
        }

    }
    private AuthenticationState BuildAuthenticatedState(TokenVM savedToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new("bearer", savedToken.Token);
        return new(new(new ClaimsIdentity(ParseClaimsFromJwt(savedToken.Token), "apiauth")));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.ReadToken(jwt) is not JwtSecurityToken tokenS) return null;

        var username = tokenS.Claims.FirstOrDefault(claim => claim.Type == "username");
        var id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Id");
        return new Claim[]
         {
            new("Id", id?.Value),
            new("username", username?.Value),
         };
    }

    public async Task<UserVM> GetCurrentUser()
    {
        var authState = await GetAuthenticationStateAsync();
        if (authState.User == null || !authState.User.Identity.IsAuthenticated) return null;

        var userClaims = authState.User.Claims.ToList();
        var firstid = userClaims.FirstOrDefault(claim => claim.Type == "Id");
        var parentLoginUserName = userClaims.FirstOrDefault(claim => claim.Type == "username");
        var currentUserVm = new UserVM
        {
            Id = firstid != null ? firstid.Value.ToGuid() : Guid.Empty,
            Username = parentLoginUserName?.Value,
        };
        if (currentUserVm.Id != Guid.Empty)
        {
            var user = await GetUser(currentUserVm.Id);

            if (user != null)
            {
                currentUserVm = user;
                currentUserVm.Username = user.Username;
            }
        }

        return currentUserVm;
    }
    public async Task<UserVM> GetUser(Guid userId)
      => await _httpClient.HandleResponse<UserVM>($"v1/ServiceConsumer/get/{userId}");
    public async Task Logout()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _localStorage.RemoveItemAsync(TokenKey);
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
    }
}
