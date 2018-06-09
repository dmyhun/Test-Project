using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using System.Data.Entity;
using TestProject.Data.Context;
using TestProject.Data.Repositories;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private RelationsRepository relationsRepository = new RelationsRepository();

        [HttpGet]
        public ActionResult Index(Guid? categoryId, string sort, string order)
        {
            HomeViewModel viewModel = new HomeViewModel();
            var relations = relationsRepository.FindRelations(categoryId, sort, order);
            List<RelationViewModel> relVMList = new List<RelationViewModel>();
            viewModel.Relations = relVMList;

            foreach (var relation in relations)
            {
                RelationViewModel relVM = new RelationViewModel()
                {
                    Id = relation.Id,
                    Name = relation.Name,
                    FullName = relation.FullName,
                    Email = relation.EMailAddress,
                    TelephoneNumber = relation.TelephoneNumber,
                    City = relation.DefaultCity,
                    Country = relation.DefaultCountry,
                    PostalCode = relation.DefaultPostalCode,
                    Street = relation.DefaultStreet,
                    StreetNumber = relationsRepository.GetStreetNumberByRelationId(relation.Id),
                    IsDisabled = relation.IsDisabled
                };
                relVMList.Add(relVM);
            }

            IEnumerable<string> KeywordsForSort = new List<string>()
            {
                "None","Name","Full Name","Telephone Number","Email","Country","City","Street","Postal Code","Street Numder"
            };
            viewModel.KeywordsForSort = new SelectList(KeywordsForSort);

            List<CategoriesForFilter> categoriesForFiter = new List<CategoriesForFilter>();

            categoriesForFiter.Add(new CategoriesForFilter() { Name = "None", CategoryId = null });

            foreach (var category in relationsRepository.GetAllCategories())
            {
                categoriesForFiter.Add(new CategoriesForFilter() { CategoryId = category.Id, Name = category.Name });
            }

            SelectList categoreisSelectList = new SelectList(categoriesForFiter, "CategoryId", "Name");

            viewModel.CategoriesList = categoreisSelectList;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            RelationManageViewModel relationManageModel = new RelationManageViewModel()
            {
                Countries = relationsRepository.GetAllCountries()
            };

            return View("Manage", relationManageModel);
        }

        [HttpPost]
        public ActionResult Create(RelationManageViewModel relation)
        {
            if (ModelState.IsValid)
            {
                var dbRelation = GetDbRelation(relation);
                dbRelation.Id = Guid.NewGuid();
                dbRelation.CreatedAt = DateTime.Now;
                dbRelation.CreatedBy = "Admin";
                dbRelation.Name = relation.Name;
                relationsRepository.CreateRelation(dbRelation, relation.StreetNumber);

                return RedirectToAction("Index");
            }

            relation.Countries = relationsRepository.GetAllCountries();

            return View("Manage", relation);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Relation dbRelation = relationsRepository.GetById(id);
            RelationManageViewModel model = GetRelationModel(dbRelation);
            model.StreetNumber = relationsRepository.GetStreetNumberByRelationId(id);
            model.Countries = relationsRepository.GetAllCountries();

            return View("Manage", model);
        }

        [HttpPost]
        public ActionResult Edit(RelationManageViewModel relation)
        {
            if (ModelState.IsValid)
            {
                var existingRelation = relationsRepository.GetById(relation.Id.Value);
                var dbRelation = GetDbRelation(relation);
                dbRelation.ModifiedBy = "Admin";
                relationsRepository.EditRelation(dbRelation, relation.StreetNumber);

                return RedirectToAction("Index");
            }

            relation.Countries = relationsRepository.GetAllCountries();

            return View("Manage", relation);
        }

        [HttpPost]
        public ActionResult DeleteRelations(List<Guid> relations)
        {
            foreach (var id in relations)
            {
                relationsRepository.DeleteRelation(id);
            }

            return new EmptyResult();
        }

        private Relation GetDbRelation(RelationManageViewModel relation)
        {
            Relation dbRelation = new Relation();

            dbRelation.Id = relation.Id.GetValueOrDefault();
            dbRelation.Name = relation.Name;
            dbRelation.FullName = relation.FullName;
            dbRelation.TelephoneNumber = relation.TelephoneNumber;
            dbRelation.EMailAddress = relation.Email;
            dbRelation.DefaultCity = relation.City;
            dbRelation.DefaultCountry = relation.Country;
            dbRelation.DefaultPostalCode = GetPostalCode(relation);
            dbRelation.DefaultStreet = relation.Street;

            return dbRelation;
        }

        private RelationManageViewModel GetRelationModel(Relation dbRelation)
        {
            RelationManageViewModel relationModel = new RelationManageViewModel
            {
                Id = dbRelation.Id,
                Name = dbRelation.Name,
                FullName = dbRelation.FullName,
                TelephoneNumber = dbRelation.TelephoneNumber,
                Email = dbRelation.EMailAddress,
                City = dbRelation.DefaultCity,
                Country = dbRelation.DefaultCountry,
                PostalCode = dbRelation.DefaultPostalCode,
                Street = dbRelation.DefaultStreet
            };
            return relationModel;
        }

        private string GetPostalCode(RelationManageViewModel relation)
        {
            StringBuilder postalCodeFormated = new StringBuilder();
            string postalCodeFormat = relationsRepository.GetPostalCodeByCountryName(relation.Country);

            if (relation.PostalCode != null && postalCodeFormat != null && postalCodeFormat.Length >= relation.PostalCode.Length)
            {
                int position = 0;

                for (int i = 0; i < postalCodeFormat.Length; i++)
                {
                    switch (postalCodeFormat[i])
                    {
                        case 'N':
                            if (Char.IsDigit(relation.PostalCode[position]))
                            {
                                postalCodeFormated.Append(relation.PostalCode[position]);
                                position++;
                            }
                            break;
                        case 'L':
                            if (Char.IsLetter(relation.PostalCode[position]))
                            {
                                postalCodeFormated.Append(relation.PostalCode[position].ToString().ToUpper());
                                position++;
                            }
                            break;
                        case 'l':
                            if (Char.IsLetter(relation.PostalCode[position]))
                            {
                                postalCodeFormated.Append(relation.PostalCode[position].ToString().ToLower());
                                position++;
                            }
                            break;
                        case ' ':
                            postalCodeFormated.Append(' ');
                            if ((relation.PostalCode[position]) == ' ')
                                position++;
                            break;
                        case '-':
                            postalCodeFormated.Append('-');
                            if ((relation.PostalCode[position]) == '-')
                                position++;
                            break;

                        case '_':
                            postalCodeFormated.Append('_');
                            if ((relation.PostalCode[position]) == '_')
                                position++;
                            break;
                        default:
                            break;
                    }
                }

                postalCodeFormated = (postalCodeFormated.Length == postalCodeFormat.Length) ? postalCodeFormated : postalCodeFormated.Clear().Append(relation.PostalCode);
            }
            else
            {
                postalCodeFormated.Clear().Append(relation.PostalCode);
            }

            return postalCodeFormated.ToString();
        }
    }
}