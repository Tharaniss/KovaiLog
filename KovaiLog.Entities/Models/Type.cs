using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KovaiLog.Entities.Models
{
    public class TypeMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Type Color")]
        public string TypeColor { get; set; }

       //public ICollection<Content> Content { get; set; }
    }
}