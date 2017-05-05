using System;
using System.Collections.Generic;

namespace UniShop.Web.Models
{
    public class PostViewModel
    {
        public int ID { set; get; }


        public string Name { set; get; }


        public string Alias { set; get; }


        public int CategoryID { set; get; }


        public string Image { set; get; }


        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }


        public virtual PostCategoryViewModel PostCategory { get; set; }

        public virtual IEnumerable<PostTagViewModel> PostTags { get; set; }

        public DateTime? CreatedDate { get; set; }


        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }


        public string UpdatedBy { get; set; }


        public string MetaKeyword { get; set; }


        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}