using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void EditPersonData(PersonData editedPersonData)
        {
            db.Entry(editedPersonData).State = EntityState.Modified;
            db.SaveChanges();
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
