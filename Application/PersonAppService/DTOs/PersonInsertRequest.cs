using System;
using System.Collections.Generic;

namespace ApplicationLayer.PersonAppService.DTOs
{
    public class PersonInsertRequest
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<long> Phones { get; set; }
    }
}
