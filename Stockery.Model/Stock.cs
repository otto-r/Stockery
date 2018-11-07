﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stockery.Model
{
    public class Stock
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "⚠️ The stock must have a name")]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MaxLength(8)]
        public string Ticker { get; set; }
    }
}
