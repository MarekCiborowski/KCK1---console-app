using DatabaseLayer;
using DataTransferObjects.Models;
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

        public PersonData CreatePersonData(
            
            string _address,
            string _city,
            string _zipcode,
            string _state,
            string _country)
        {
            return new PersonData
            {
                
                address = _address,
                city = _city,
                zipcode = _zipcode,
                state = _state,
                country = _country,
            };
        }
        public PersonData GetPersonData(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.personDatas.FirstOrDefault(p => p.personDataID == id);
        }

        public void AddPersonData(PersonData personData)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.personDatas.Add(personData);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public void EditPersonData(PersonData editedPersonData)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(editedPersonData).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public void RemovePersonData(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    PersonData personData = db.personDatas.Find(id);
                    db.personDatas.Remove(personData);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
