using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestTask.Frames
{
    /// <summary>
    /// Interaction logic for Convertor.xaml
    /// </summary>
    public partial class Convertor : Page
    {
        public Convertor()
        {
            InitializeComponent();
        }

        private void CountCurrency_Loaded(object sender, RoutedEventArgs e)
        {
            var cryptocurrenciesIds = DataApp.cryptocurrencies.Select(x => x.Id);
            FromCurrency.ItemsSource = cryptocurrenciesIds;
            ToCurrency.ItemsSource = cryptocurrenciesIds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(CountCurrency.Text, out decimal countCurrency))
            {
                var fromIdCrypt = FromCurrency.SelectedItem.ToString();
                var toIdCrypt = ToCurrency.SelectedItem.ToString();
                if (fromIdCrypt!=toIdCrypt&&fromIdCrypt!=null) {
                    var fromCurrency = DataApp.cryptocurrencies.Find(x => x.Id.Equals(fromIdCrypt));
                    var toCurrency = DataApp.cryptocurrencies.Find(x => x.Id.Equals(toIdCrypt));
                    var summFrom = countCurrency * fromCurrency.PriceUsd;
                    var countTo = summFrom / toCurrency.PriceUsd;
                    ResultCurrency.Content = Math.Round(countTo, 5);
                    ResultUSD.Content = summFrom.ToString("C2");
                }
            }
            
        }
    }
}
