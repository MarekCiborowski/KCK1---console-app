using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class PersonDataRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public PersonData GetPersonData(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.personData.FirstOrDefault(p => p.PersonDataID == id);
        }

        public void AddPersonData(PersonData personData)
        {
            db.personData.Add(personData);
            db.SaveChanges();
        }

        public PersonData EditPersonData(PersonData editedPersonData)
        {
            PersonData personData = db.personData.Find(editedPersonData.PersonDataID);
            personData.Address = editedPersonData.Address;
            personData.City = editedPersonData.City;
            personData.Zipcode = editedPersonData.Zipcode;
            personData.State = editedPersonData.State;
            personData.Country = editedPersonData.Country;
            db.SaveChanges();
            return personData;
        }

        public void RemovePersonData(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            PersonData personData = db.personData.Find(id);
            db.personData.Remove(personData);
            db.SaveChanges();
        }
    }
}
