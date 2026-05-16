using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestApiUtil;

public static class RestUtil
{
    public static JsonSerializerOptions JsonOptions { get; set; }
            = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    public static T Get<T>(string baseUri, string relativeUri)
    {
        HttpClient client = HttpClientCache.Get(baseUri);

        var result = client.GetAsync(relativeUri).Result;

        if (!result.IsSuccessStatusCode)
            throw CreateException(result);

        var content = result.Content.ReadAsStringAsync().Result;
        T jsonObject = JsonSerializer.Deserialize<T>(content, JsonOptions)!;
        return jsonObject;
    }
    private static Exception CreateException(HttpResponseMessage result)
    {
        string content = result.Content.ReadAsStringAsync().Result;
        string reqMsg = result.RequestMessage!.Method.ToString()
            + " : " + result.RequestMessage.RequestUri!.ToString();
        string? reqBody = result.RequestMessage?.Content?.ReadAsStringAsync().Result;
        Exception inner = new Exception(reqMsg + " : " + (reqBody ?? ""));

        string reason = content != "" ? content : reqMsg;
        return new Exception(
            (int)result.StatusCode + "-" + result.StatusCode.ToString()
            + " : " + reason, inner);
    }
    public static void Post<T>(string baseUri, string relativeUri, T data)
    {
        HttpClient client = HttpClientCache.Get(baseUri);

        HttpContent contentIn = JsonContent.Create(data);
        var result = client.PostAsync(relativeUri, contentIn).Result;

        if (!result.IsSuccessStatusCode)
            throw CreateException(result);
    }
    public static TResult PostWithResult<TInput, TResult>(string baseUri, string relativeUri, TInput? data)
    {
        HttpClient client = HttpClientCache.Get(baseUri);

        HttpContent? contentIn = (data != null ? JsonContent.Create(data) : null);
        var result = client.PostAsync(relativeUri, contentIn).Result;

        if (!result.IsSuccessStatusCode)
            throw CreateException(result);

        var content = result.Content.ReadAsStringAsync().Result;
        TResult jsonObject = JsonSerializer.Deserialize<TResult>(content, JsonOptions)!;
        return jsonObject;
    }
    public static void Patch<T>(string baseUri, string relativeUri, T data)
    {
        HttpClient client = HttpClientCache.Get(baseUri);

        HttpContent contentIn = JsonContent.Create(data);
        var result = client.PatchAsync(relativeUri, contentIn).Result;

        if (!result.IsSuccessStatusCode)
            throw CreateException(result);
    }
    public static void Put<T>(string baseUri, string relativeUri, T data)
    {
        HttpClient client = HttpClientCache.Get(baseUri);

        HttpContent contentIn = JsonContent.Create(data);
        var result = client.PutAsync(relativeUri, contentIn).Result;

        if (!result.IsSuccessStatusCode)
            throw CreateException(result);
    }
    public static void Delete(string baseUri, string relativeUri)
    {
        HttpClient client = HttpClientCache.Get(baseUri);

        var result = client.DeleteAsync(relativeUri).Result;

        if (!result.IsSuccessStatusCode)
            throw CreateException(result);
    }
}
