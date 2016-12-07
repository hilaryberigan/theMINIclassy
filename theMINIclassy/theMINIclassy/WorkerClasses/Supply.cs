using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace theMINIclassy.WorkerClasses
{
    public class Supply
    {
        [Key]
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Quantity { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public int MinThreshold { get; set; }
    }
}