using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniShop.Model.Models;

namespace UniShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { get; set; }

        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }

        [MaxLength(256)]
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }
        public bool Status { get; set; }

        [StringLength(128)]
        public string CustomerId { get; set; }

        public string BankCode { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}