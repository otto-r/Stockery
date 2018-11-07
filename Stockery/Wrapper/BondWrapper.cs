using Stockery.Model;
using Stockery.Wrapper;
using System;
using System.Collections.Generic;

namespace Stockery.Wrapper
{
    public class BondWrapper : ModelWrapper<Bond>
    {
        public BondWrapper(Bond model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }


        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Bond", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Bonds cannot be named \"bond\"";
                    }
                    break;
            }
        }
    }
}
