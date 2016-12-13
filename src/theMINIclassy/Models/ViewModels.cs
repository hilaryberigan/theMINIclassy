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
    public class StyleViewModel
    {
        public Style Style { get; set; }

        public List<PatPieceStyle> ShowPatPieces { get; set; }

        public List<PatternPiece> SelectedPatPieces { get; set; }

        public PatternPiece PatPiece { get; set; }

    }

    public class SupplyViewModel
    {
        public List<SupplyObj> Supplies { get; set; }
    }

    public class SupplyObj

    {
        public string Title { get; set; }
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Type { get; set; }
        public string Measure { get; set; }
    }


}