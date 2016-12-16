using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theMINIclassy.Models.ManageViewModels
{
    public class PatPieceViewModel
    {
        public PatternPiece PatternPiece { get; set; }

        public List<Style> Styles { get; set; }

        public List<PatPieceStyle> PatPieceStyles { get; set; }

        public List<Variation> Variations { get; set; }

        public List<PatPieceVariation> PatPieceVariations { get; set; }

        public List<PatternPiece> PatPieces { get; set; }

        public Style Style { get; set; }

        public Variation Variation { get; set; }

    }
}
