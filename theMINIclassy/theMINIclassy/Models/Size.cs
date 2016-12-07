using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace theMINIclassy.Models
{
    public class Size
    {
        [Key]
        public int? Id { get; set; }

        public string ItemSize { get; set; }

        public string Code { get; set; }

    }
}