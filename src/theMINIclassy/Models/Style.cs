using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace theMINIclassy.Models
{
    public class Style
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

    }
}