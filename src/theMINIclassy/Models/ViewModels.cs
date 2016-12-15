using System;
using System.Collections.Generic;
using System.Linq;

namespace theMINIclassy.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }  

        public List<ProductQuantity> PQuantities { get; set; }

        public List<Product> Products { get; set; }

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