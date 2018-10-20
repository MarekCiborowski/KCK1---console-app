using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class PersonDataService
    {
        private PersonDataRepository personDataR = new PersonDataRepository();

        

        public PersonData GetPersonData(int? id)
        {
            return personDataR.GetPersonData(id);
        }

        public void AddPersonData(PersonData personData)
        {
            personDataR.AddPersonData(personData);
        }

        public void DeleteAccount(int? id)
        {
            personDataR.RemovePersonData(id);
        }

        public void EditAccount(PersonData personData)
        {
            personDataR.EditPersonData(personData);
        }

    }
}
