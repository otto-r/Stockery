using Stockery.Model;
using System;
using System.Collections.Generic;

namespace Stockery.Wrapper
{
    public class StockWrapper : ModelWrapper<Stock>
    {
        public StockWrapper(Stock model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }


        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Ticker
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Stock", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Stocks cannot be named \"stock\"";
                    }
                    break;
            }
        }
    }
}
