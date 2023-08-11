using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Model
{
   public class CryptocurrencyModelView:Cryptocurrency{
        public CandlesSource CandlesticksChart { get; set; }
}
}
