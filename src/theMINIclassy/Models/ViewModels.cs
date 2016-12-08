using System;
using System.Collections.Generic;
using System.Linq;

namespace theMINIclassy.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int OrderNumber { get; set; }

        public string OriginatedFrom { get; set; }

        public string OrderStatus { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<string> Products { get; set; }
    }
}