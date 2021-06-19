using System;
using System.Collections.Generic;

namespace DomainLayer.Entities.PersonModel.Commands
{
    public class PersonDomainCreateUpdateCommand
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<long> Phones { get; set; }
    }
}