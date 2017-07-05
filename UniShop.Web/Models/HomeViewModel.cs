using System.Collections.Generic;

namespace UniShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> LastestProducts { get; set; }
        public IEnumerable<ProductViewModel> HotProducts { get; set; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}