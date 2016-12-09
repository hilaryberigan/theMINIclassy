using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace theMINIclassy.Models
{
    public class Collaborator
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}