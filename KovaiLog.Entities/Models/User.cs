using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KovaiLog.Entities.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be 10 characters or less"), MinLength(5)]
        [Index(IsUnique = true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be 10 characters or less"), MinLength(1)]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}