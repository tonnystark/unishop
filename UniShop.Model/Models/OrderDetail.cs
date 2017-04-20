using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniShop.Model.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        public int OrderID { get; set; }

        [Key, Column(Order = 1)]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
