using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestTask.Api;
using TestTask.Model;


namespace TestTask
{
    public class CoinCapApi : ICryptoApi
    {
        private HttpClient httpClient
        {
            get { return new HttpClient(); }
        }

        public string urlCoinCap = "https://api.coincap.io/v2/";

        private async Task<T> QueryToApiAsync<T>(string url)
        {
            try
            {
                using (httpClient)
                {
                    var response = await httpClient.GetAsync(url);
                    var res = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    if (res != null)
                    {
                        T data = JsonConvert.DeserializeObject<T>(res["data"].ToString());
                        return data;
                    }

                    return default(T);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<IEnumerable<HistoryCurrency>> GetHistory(string id)
        {
            return await QueryToApiAsync<IEnumerable<HistoryCurrency>>(urlCoinCap + $"assets/{id}/history?interval=d1");
        }

        public async Task<IEnumerable<Cryptocurrency>> FetchCrypto()
        {
            return await QueryToApiAsync<IEnumerable<Cryptocurrency>>(urlCoinCap + "assets");
        }

        public async Task<Cryptocurrency> GetCryptoById(string id)
        {
            return await QueryToApiAsync<Cryptocurrency>(urlCoinCap + $"assets/{id}");
        }

        public async Task<IEnumerable<MarketsCurrency>> GetMarketCurrencies(string id)
        {
            return await QueryToApiAsync<IEnumerable<MarketsCurrency>>(urlCoinCap + $"assets/{id}/markets");
        }

        public async Task<IEnumerable<Candlestick>> GetCryptoCandlesticks(string id)
        {
            return await QueryToApiAsync<IEnumerable<Candlestick>>(urlCoinCap +
                                                                   $"candles?exchange=poloniex&interval=m5&baseId={id}&quoteId=bitcoin");
        }
    }
}