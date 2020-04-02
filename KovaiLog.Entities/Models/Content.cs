using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KovaiLog.Entities.Models
{
    public class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContentId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Content Title")]
        public string ContentTitle { get; set; }

        [Required]
        [Display(Name = "Content Type")]
        public int ContentType { get; set; }

        [Required]
        [Display(Name = "Content Description")]
        [MaxLength(1000)]
        public string ContentDesc { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("ContentType")]
        public TypeMaster ContentTypes { get; set; }

        [NotMapped]
        public string CreatedDate { get; set; }

    }
}