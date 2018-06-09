using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Models
{
    public class RelationViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string TelephoneNumber { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public int? StreetNumber { get; set; }

        public bool IsDisabled { get; set; }
    }
}