using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiUtil;

// some code from https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
public static class HttpClientCache
{

    static Dictionary<string, HttpClient> dict = new();
    public static HttpClient Get(string baseUri)
    {
        HttpClient? client;
        if (dict.TryGetValue(baseUri, out client))
            return client;

        client = CreateClient(baseUri);
        dict.Add(baseUri, client);
        return client;
    }

    private static HttpClient CreateClient(string baseUri)
    {
        SocketsHttpHandler handler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Recreate every 15 minutes
        };
        HttpClient client = new HttpClient(handler);
        client.BaseAddress = new Uri(baseUri);

        return client;
    }
}
