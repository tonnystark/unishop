﻿using System;

namespace UniShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
}