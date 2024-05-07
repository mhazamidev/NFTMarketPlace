using Newtonsoft.Json;
using NFTMarketPlace.Common.Exceptions;
using System.Dynamic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace NFTMarketPlace.Common.Utility;

public static class ResponseUtils
{
    public static async Task<TResult> HandleResponse<TResult>(this HttpResponseMessage result)
    {


        switch (result.StatusCode)
        {
            case System.Net.HttpStatusCode.Accepted:
                break;
            case System.Net.HttpStatusCode.Ambiguous:
                break;
            case System.Net.HttpStatusCode.BadGateway:
                break;
            case System.Net.HttpStatusCode.InternalServerError:
            case System.Net.HttpStatusCode.BadRequest:
                var exceptionHandler = await result.Content.ReadFromJsonAsync<ExceptionHandler>();
                if (exceptionHandler != null)
                    throw exceptionHandler.GetException(exceptionHandler);
                break;
            case System.Net.HttpStatusCode.Conflict:
                break;
            case System.Net.HttpStatusCode.Continue:
                break;
            case System.Net.HttpStatusCode.Created:
                break;
            case System.Net.HttpStatusCode.ExpectationFailed:
                break;
            case System.Net.HttpStatusCode.Forbidden:
                break;
            case System.Net.HttpStatusCode.Found:
                break;
            case System.Net.HttpStatusCode.GatewayTimeout:
                break;
            case System.Net.HttpStatusCode.Gone:
                break;
            case System.Net.HttpStatusCode.HttpVersionNotSupported:
                break;


            case System.Net.HttpStatusCode.LengthRequired:
                break;
            case System.Net.HttpStatusCode.MethodNotAllowed:
                break;
            case System.Net.HttpStatusCode.Moved:
                break;
            case System.Net.HttpStatusCode.NoContent:
                return default; ;
            case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                break;
            case System.Net.HttpStatusCode.NotAcceptable:
                break;
            case System.Net.HttpStatusCode.NotFound:
                break;
            case System.Net.HttpStatusCode.NotImplemented:
                break;
            case System.Net.HttpStatusCode.NotModified:
                break;
            case System.Net.HttpStatusCode.OK:
                var response = await result.Content.ReadFromJsonAsync<ReponseSuccess<TResult>>();
                return response.Data;
            case System.Net.HttpStatusCode.PartialContent:
                break;
            case System.Net.HttpStatusCode.PaymentRequired:
                break;
            case System.Net.HttpStatusCode.PreconditionFailed:
                break;
            case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                break;
            case System.Net.HttpStatusCode.RedirectKeepVerb:
                break;
            case System.Net.HttpStatusCode.RedirectMethod:
                break;
            case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                break;
            case System.Net.HttpStatusCode.RequestEntityTooLarge:
                break;
            case System.Net.HttpStatusCode.RequestTimeout:
                break;
            case System.Net.HttpStatusCode.RequestUriTooLong:
                break;
            case System.Net.HttpStatusCode.ResetContent:
                break;
            case System.Net.HttpStatusCode.ServiceUnavailable:
                break;
            case System.Net.HttpStatusCode.SwitchingProtocols:
                break;
            case System.Net.HttpStatusCode.Unauthorized:
                break;
            case System.Net.HttpStatusCode.UnsupportedMediaType:
                break;
            case System.Net.HttpStatusCode.Unused:
                break;
            case System.Net.HttpStatusCode.UpgradeRequired:
                break;
            case System.Net.HttpStatusCode.UseProxy:
                break;
            default:
                break;
        }
        var message = await result.Content.ReadAsStringAsync();
        throw new Exception(message);
    }


    public static async Task<TResult> HandleResponse<TResult>(this HttpClient http, string api)
    {
        var fromJsonAsync = await http.GetAsync(api);
        return await fromJsonAsync.HandleResponse<TResult>();
    }

    public static IEnumerable<dynamic> DynamicResponseToDynamic(this IEnumerable<dynamic> list)
    {
        List<dynamic> data = new List<dynamic>();



        string text = $"{string.Join(',', list)}";

        var item_maches = new Regex(@"{(.+?)}").Matches(text);
        foreach (Match match in item_maches)
        {
            dynamic obj = new ExpandoObject();
            var dItem = obj as IDictionary<string, object>;

            var item = match.Value.ToString();
            var props = item.Split(',');
            foreach (var prop in props)
            {
                var propInfo = prop.Split(":");
                var propName = propInfo[0].Replace("\"", "").Replace("{", "");
                if (propInfo.Length > 1)
                {

                }
                var value = propInfo[1].Replace("\"", "").Replace("}", "");
                dItem.Add(propName, value);
            }
            data.Add(dItem);
        }

        return data;
    }

}

internal class ReponseSuccess<TResult>
{
    public TResult Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}

