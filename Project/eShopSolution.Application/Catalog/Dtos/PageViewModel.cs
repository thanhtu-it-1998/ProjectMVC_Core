﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Dtos
{
   public class PageViewModel<T>
    {
       public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
