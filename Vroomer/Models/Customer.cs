using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vroomer.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage = "ID is required.")]
        public string id { get; set; }
        [Required(ErrorMessage = "name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "last name is required.")]
        [DisplayName("Last name")]
        public string surname { get; set; }
    }
}
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;

//namespace Vroomer.Models
//{
//    public class Customer
//    {
//        [Key]
//        [Required(ErrorMessage = "ID is required.")]
//        public string Id { get; set; }

//        [Required(ErrorMessage = "Name is required.")]
//        public string Name { get; set; }

//        [Required(ErrorMessage = "Last name is required.")]
//        [DisplayName("Last Name")]
//        public string Surname { get; set; }
//    }
//}
