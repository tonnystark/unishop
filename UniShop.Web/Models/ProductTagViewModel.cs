namespace UniShop.Web.Models
{
    public class ProductTagViewModel
    {
        public int ProductID { get; set; }


        public string TagID { get; set; }


        public virtual ProductViewModel Product { get; set; }

        public virtual TagViewModel Tag { get; set; }
    }
}