
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestTask.Model;
using System.Windows.Documents;
using System;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using TestTask.Extensions;
using AutoMapper;
using FancyCandles;
using System.Reflection.Metadata;

namespace TestTask.Frames
{
    /// <summary>
    /// Логика взаимодействия для Cryptos.xaml
    /// </summary>
    public partial class Cryptos : Page
    {
        private bool firstLoad = true;
        private readonly IMapper mapper;
        private CryptocurrencyModelView cryptocurrency { get; set; }
        CandlesSource candles;
        public Cryptos(Cryptocurrency cryptocurrency)
        {
            InitializeComponent();
            mapper = new Mapper(new MapperConfiguration(conf =>
            conf.AddProfile(new MapperProfile())));
            
            this.cryptocurrency = mapper.Map<CryptocurrencyModelView>(cryptocurrency);

            FancyCandles.TimeFrame timeFrame = FancyCandles.TimeFrame.M5;
            CandlesSource candles = new CandlesSource(timeFrame);
            this.cryptocurrency.CandlesticksChart = candles;
            DataContext = this.cryptocurrency;
        }
        private async void TickLoading(object sender, EventArgs e)
        {
            ProgressBar.IsIndeterminate = true;
            await UpdateData();
            ProgressBar.IsIndeterminate = false;
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeLoadingState(true);
            await UpdateData();
            ChangeLoadingState(false);
        }

        private void ChangeLoadingState(bool state)
        {
            LoadingLayout.Visibility = state ? Visibility.Visible : Visibility.Hidden;
            ProgressBar.IsIndeterminate = state;
        }

        private async Task UpdateData()
        {
            await FetchData();
            HistoryTable.ItemsSource = cryptocurrency.HistoryCurrencies;
            MarketTable.ItemsSource = cryptocurrency.MarketCurrencies;
        }
        private async Task FetchData()
        {
            cryptocurrency = mapper.Map<CryptocurrencyModelView>(await ApiClient.Api.GetCryptoById(cryptocurrency.Id));
            cryptocurrency.HistoryCurrencies = await ApiClient.Api.GetHistory(cryptocurrency.Id);
            cryptocurrency.MarketCurrencies = await ApiClient.Api.GetMarketCurrencies(cryptocurrency.Id);
            var candlesticks = await ApiClient.Api.GetCryptoCandlesticks(cryptocurrency.Id);
            cryptocurrency.CandlesticksChart = null;
            foreach (var item in candlesticks)
            {
                cryptocurrency.CandlesticksChart.Add(new CandleStickChart(item.Time, item.Open, item.High, item.Low, item.Close, item.Volume));
            }
        }
    }
}
