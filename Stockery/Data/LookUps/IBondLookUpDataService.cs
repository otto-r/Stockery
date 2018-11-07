﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Stockery.Model;

namespace Stockery.Data.LookUps
{
    public interface IBondLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetBondLookUpAsync();
    }
}