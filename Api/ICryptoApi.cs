using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Model;

namespace TestTask.Api
{
    public interface ICryptoApi
    {
        public Task<IEnumerable<HistoryCurrency>> GetHistory(string id);
        public Task<IEnumerable<Cryptocurrency>> FetchCrypto();
        public Task<Cryptocurrency> GetCryptoById(string id);
        public Task<IEnumerable<MarketsCurrency>> GetMarketCurrencies(string id);
        public Task<IEnumerable<Candlestick>> GetCryptoCandlesticks(string id);
    }
}