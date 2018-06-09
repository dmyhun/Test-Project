using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Models
{
    public class HomeViewModel
    {
        public List<RelationViewModel> Relations { get; set; }

        public SelectList KeywordsForSort { get; set; }

        public SelectList CategoriesList { get; set; }
    }
}