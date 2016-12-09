﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace theMINIclassy.Models
{
    public class PatternPiece
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}