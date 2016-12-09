using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace theMINIclassy.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string ApartmentNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

        public string GetFullAddr
        {
           
             get
            {
                return StreetNumber + " " + ApartmentNumber + " " + StreetName + ", " + City + ", " + State + " " + ZipCode;
            }
        }
    }
}
