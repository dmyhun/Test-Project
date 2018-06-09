using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Models
{
    public class RelationManageViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "The field must be set")]        
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The field can consist only of letters")]
        public string Name { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "he length of the string must be between 3 and 20 characters")]
        public string FullName { get; set; }

        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Invalid format of a phone number")]
        public string TelephoneNumber { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string Country { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "he length of the string must be between 3 and 20 characters")]
        public string City { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "he length of the string must be between 3 and 20 characters")]
        public string Street { get; set; }

        [RegularExpression(@"[0-9]{1,9}", ErrorMessage = "The field can consist only of numbers")]
        public int?  StreetNumber { get; set; }

        public string PostalCode { get; set; }

        public IList<String> Countries = new List<string>();
    }
}