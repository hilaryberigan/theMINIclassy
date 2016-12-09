using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class PatPieceStyle
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Style")]
        public int StyleId { get; set; }

        public Style Style { get; set; }

        [ForeignKey("PatternPiece")]
        public int PatPieceId { get; set; }

        public PatternPiece PatternPiece { get; set; }

    }
}