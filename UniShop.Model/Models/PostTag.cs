using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniShop.Model.Models
{
    [Table("PostTags")]
    public class PostTag
    {
        [Key, Column(Order = 0)]
        public int PostID { get; set; }

        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar", Order = 1)]
        public string TagID { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }


        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }
}