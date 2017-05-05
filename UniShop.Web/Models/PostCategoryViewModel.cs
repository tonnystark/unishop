using System;
using System.Collections.Generic;

namespace UniShop.Web.Models
{
    public class PostCategoryViewModel
    {
        public int ID { set; get; }


        public string Name { set; get; }

        public string Alias { set; get; }

        public string Description { set; get; }
        public int? ParentID { set; get; }
        public int? DisplayOrder { set; get; }


        public string Image { set; get; }

        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<PostViewModel> Posts { get; set; }

        public DateTime? CreatedDate { get; set; }


        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }


        public string UpdatedBy { get; set; }


        public string MetaKeyword { get; set; }


        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}