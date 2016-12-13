using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace theMINIclassy.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Item Size")]
        public string ItemSize { get; set; }

        public string Code { get; set; }

    }
}