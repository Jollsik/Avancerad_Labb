﻿using Avancerad_Labb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.ViewModels
{
    public class CartViewModel
    {
        public List<string> products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
