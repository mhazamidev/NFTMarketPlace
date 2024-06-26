﻿using Microsoft.JSInterop;
using NFTMarketPlace.MetaMask.Enums;
using NFTMarketPlace.MetaMask.Exceptions;
using NFTMarketPlace.MetaMask.Extensions;
using System.Numerics;
using System.Text.Json;

namespace NFTMarketPlace.MetaMask;

public class MetaMaskService : IAsyncDisposable, IMetaMaskService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;


    public MetaMaskService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => LoadScripts(jsRuntime).AsTask());
    }

    public ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime)
    {
        return jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/NFTMarketPlace.MetaMask/metaMaskJsInterop.js");
    }

    public async ValueTask ConnectMetaMask()
    {
        var module = await moduleTask.Value;
        try
        {
            await module.InvokeVoidAsync("checkMetaMask");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<bool> HasMetaMask()
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<bool>("hasMetaMask");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<bool> IsSiteConnected()
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<bool>("isSiteConnected");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask ListenToEvents()
    {
        var module = await moduleTask.Value;
        try
        {
            await module.InvokeVoidAsync("listenToChangeEvents");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> GetSelectedAddress()
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("getSelectedAddress", null);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<(long chainId, Chain chain)> GetSelectedChain()
    {
        var module = await moduleTask.Value;
        try
        {
            string chainHex = await module.InvokeAsync<string>("getSelectedChain", null);
            return IMetaMaskService.ChainHexToChainResponse(chainHex);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<long> GetTransactionCount()
    {
        var module = await moduleTask.Value;
        try
        {
            var result = await module.InvokeAsync<JsonElement>("getTransactionCount");
            var resultString = result.GetString();
            if (resultString != null)
            {
                long intValue = resultString.HexToInt();
                return intValue;
            }
            return 0;
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> PersonalSign(string message)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("personalSign", message);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> SignTypedData(string label, string value)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("signTypedData", label, value);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> SignTypedDataV4(string typedData)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("signTypedDataV4", typedData);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> SendTransaction(string to, BigInteger weiValue, string? data = null)
    {
        var module = await moduleTask.Value;
        try
        {
            string hexValue = "0x" + weiValue.ToString("X");
            return await module.InvokeAsync<string>("sendTransaction", to, hexValue, data);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<dynamic> GenericRpc(string method, params dynamic?[]? args)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<dynamic>("genericRpc", method, args);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async Task<string> RequestAccounts()
    {
        var result = await GenericRpc("eth_requestAccounts");

        return result.ToString();
    }

    public async Task<BigInteger> GetBalance(string address, string block = "latest")
    {
        var result = await GenericRpc("eth_getBalance", address, block);

        string hex = result.ToString();

        return hex.HexToBigInteger();
    }

    //[JSInvokable()]
    //public static async Task OnConnect()
    //{
    //    Console.WriteLine("connected");
    //    if (ConnectEvent != null)
    //    {
    //        await ConnectEvent.Invoke();
    //    }
    //}

    //[JSInvokable()]
    //public static async Task OnDisconnect()
    //{
    //    Console.WriteLine("disconnected");
    //    if (DisconnectEvent != null)
    //    {
    //        await DisconnectEvent.Invoke();
    //    }
    //}

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    private void HandleExceptions(Exception ex)
    {
        switch (ex.Message)
        {
            case "NoMetaMask":
                throw new NoMetaMaskException();
            case "UserDenied":
                throw new UserDeniedException();
        }
    }
}
