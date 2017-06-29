﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> LastestProducts { get; set; }
        public IEnumerable<ProductViewModel> HotProducts { get; set; }
    }
}