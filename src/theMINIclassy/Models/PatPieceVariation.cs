using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class PatPieceVariation
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Variation")]
        public int VariationId { get; set; }

        public Variation Variation { get; set; }

        [ForeignKey("PatternPiece")]
        public int PatPieceId { get; set; }

        public PatternPiece PatternPiece { get; set; }
    }
}