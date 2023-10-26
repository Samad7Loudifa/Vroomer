using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vroomer.Models
{
    public class RentaCar
    {

        [Key]
        [Required(ErrorMessage = "Field is required.")]
        public int RentalId { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Field is required.")] 
        public int CarId { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        
    }
  
}
