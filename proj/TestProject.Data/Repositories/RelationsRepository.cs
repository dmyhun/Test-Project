using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data.Context;

namespace TestProject.Data.Repositories
{
    public class RelationsRepository
    {
        public List<Relation> FindRelations(Guid? categoryId, string sortBy, string order)
        {
            using (DbModel context = new DbModel())
            {
                var relations = context.Relations.Where(x => x.IsDisabled == false);

                if (categoryId != null)
                {
                    var relationCategories = context.RelationCategories.Where(e => e.CategoryId == categoryId).Select(e => e.RelationId).ToList();

                    relations = relations.Where(e => relationCategories.Contains(e.Id));
                }

                if (sortBy != null)
                {
                    switch (sortBy.Replace(" ", string.Empty).ToUpper())
                    {
                        case "NAME":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.Name) : relations.OrderByDescending(e => e.Name);
                            break;
                        case "FULLNAME":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.FullName) : relations.OrderByDescending(e => e.FullName);
                            break;
                        case "TELEPHONENUMBER":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.TelephoneNumber) : relations.OrderByDescending(e => e.TelephoneNumber);
                            break;
                        case "EMAIL":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.EMailAddress) : relations.OrderByDescending(e => e.EMailAddress);
                            break;
                        case "COUNTRY":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.DefaultCountry) : relations.OrderByDescending(e => e.DefaultCountry);
                            break;
                        case "CITY":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.DefaultCity) : relations.OrderByDescending(e => e.DefaultCity);
                            break;
                        case "STREET":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.DefaultStreet) : relations.OrderByDescending(e => e.DefaultStreet);
                            break;
                        case "POSTALCODE":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.DefaultPostalCode) : relations.OrderByDescending(e => e.DefaultPostalCode);
                            break;
                        case "STREETNUMBER":
                            relations = order.ToUpper() == "ASC" ? relations.OrderBy(e => e.DefaultStreet) : relations.OrderByDescending(e => e.DefaultStreet);
                            break;
                    }
                }

                return relations.ToList();
            }
        }

        public Relation GetById(Guid id)
        {
            using (DbModel context = new DbModel())
            {
                return context.Relations.Where(e => e.Id == id).First();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (DbModel context = new DbModel())
            {
                return context.Categories.ToList();
            }
        }

        public void CreateRelation(Relation relation, int? streetNumber)
        {
            using (DbModel context = new DbModel())
            {
                context.Relations.Add(relation);
                context.RelationAddresses.Add(new RelationAddress()
                {
                    RelationId = relation.Id,
                    Number = streetNumber,
                    AddressTypeId = new Guid("00000000-0000-0000-0000-000000000002")
                });
                context.SaveChanges();
            }
        }

        public void EditRelation(Relation relation, int? streetNumber)
        {
            using (DbModel context = new DbModel())
            {
                var dbRelation = context.Relations.Where(e => e.Id == relation.Id).First();

                dbRelation.FullName = relation.FullName;
                dbRelation.TelephoneNumber = relation.TelephoneNumber;
                dbRelation.EMailAddress = relation.EMailAddress;
                dbRelation.DefaultCity = relation.DefaultCity;
                dbRelation.DefaultCountry = relation.DefaultCountry;
                dbRelation.DefaultPostalCode = relation.DefaultPostalCode;
                dbRelation.DefaultStreet = relation.DefaultStreet;

                if (streetNumber != null)
                {
                    var dbRelAdress = context.RelationAddresses.Where(e => e.RelationId == relation.Id).First();
                    dbRelAdress.Number = streetNumber;
                }

                context.SaveChanges();
            }
        }

        public void DeleteRelation(Guid relationId)
        {
            using (DbModel context = new DbModel())
            {


                var dbRelation = context.Relations.Where(e => e.Id == relationId).First();
                dbRelation.IsDisabled = true;
                context.SaveChanges();
            }
        }

        public List<string> GetAllCountries()
        {
            using (DbModel context = new DbModel())
            {
                return context.Countries.Select(e => e.Name).ToList();
            }
        }

        public string GetPostalCodeByCountryName(string countryName)
        {
            using (DbModel db = new DbModel())
            {
                return db.Countries.Where(c => c.Name == countryName).First().PostalCodeFormat;
            }
        }

        public int? GetStreetNumberByRelationId(Guid relationId)
        {
            using (DbModel context = new DbModel())
            {
                return context.RelationAddresses.Where(c => c.RelationId == relationId).First().Number;
            }
        } 
    }
}