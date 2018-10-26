using System.Collections.Generic;

namespace Stockery.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public List<double> HistoricalPrices { get; set; }
    }
}
