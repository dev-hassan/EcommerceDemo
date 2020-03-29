using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Displayed Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Displayed Order")]
        public int DisplayOrder { get; set; }
    }
}
