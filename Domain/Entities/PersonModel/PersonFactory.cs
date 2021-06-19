using DomainLayer.Entities.PersonModel.Commands;
using DomainLayer.Resources;
using Support.ExceptionsManagement;
using System.Linq;

namespace DomainLayer.Entities.PersonModel
{
    public static class PersonFactory
    {
        public static Person Create(this PersonDomainCreateUpdateCommand personDomainCreateUpdateCommand)
        {
            personDomainCreateUpdateCommand.CheckIntegrity();

            var person = new Person()
            {
                Name = personDomainCreateUpdateCommand.Name,
                Birthday = personDomainCreateUpdateCommand.Birthday.Date
            };

            person.Add(personDomainCreateUpdateCommand.Phones.Select(phone => phone));

            return person;
        }

        public static void Update(this Person person, PersonDomainCreateUpdateCommand personDomainCreateUpdateCommand)
        {
            personDomainCreateUpdateCommand.CheckIntegrity();

            person.Name = personDomainCreateUpdateCommand.Name;
            person.Birthday = personDomainCreateUpdateCommand.Birthday.Date;

            person.Add(personDomainCreateUpdateCommand.Phones.Select(phone => phone));
        }

        private static void CheckIntegrity(
            this PersonDomainCreateUpdateCommand personDomainCreateUpdateCommand)
        {
            var integrityCheckup = new IntegrityCheckup();
            integrityCheckup.CheckRequired(personDomainCreateUpdateCommand.Name, string.Format(DomainMessages.Required, "Name"));
            integrityCheckup.CheckRequired(personDomainCreateUpdateCommand.Birthday, string.Format(DomainMessages.Required, "Birthday"));
            integrityCheckup.CheckFilledList(personDomainCreateUpdateCommand.Phones, string.Format(DomainMessages.InformAtLeastOne, "phone number"));

            foreach (var phone in personDomainCreateUpdateCommand.Phones)
                integrityCheckup.CheckRequired(phone, string.Format(DomainMessages.Required, "phone number"));

            integrityCheckup.ThrowExceptions();
        }
    }
}