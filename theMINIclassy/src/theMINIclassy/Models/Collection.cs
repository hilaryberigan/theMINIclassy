using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace theMINIclassy.Models
{
    public class Collection
    {
        [Key]
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Month { get; set; }

        public string Code { get; set; }

        [ForeignKey("Season")]
        public int SeasonId { get; set; }

        public Season Season { get; set; }

        [ForeignKey("Collaborator")]
        public int CollaboratorId { get; set; }

        public Collaborator Collaborator { get; set; }


    }
}