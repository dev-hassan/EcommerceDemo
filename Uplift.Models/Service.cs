using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uplift.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Service Name")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [DisplayName("Description")]
        public string LongDesc { get; set; }
        
        [DataType(DataType.ImageUrl)]
        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [ForeignKey("Category")]
        public int CategouryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Frequency")]
        public int FrequencyId { get; set; }

        public Frequency Frequency { get; set; }
    }
}
