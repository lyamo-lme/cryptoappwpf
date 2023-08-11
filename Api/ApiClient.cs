using System;
using System.Collections.Generic;
using TestTask.Api;

namespace TestTask;

public static class ApiClient
{
    private static Dictionary<string, ICryptoApi> CryptoApis = new Dictionary<string, ICryptoApi>()
    {
        {
            "coincapapi", new CoinCapApi()
        }
    };

    public static string api = "coincapapi";

    public static ICryptoApi Api
    {
        get { return CryptoApis[api]; }
    }
}