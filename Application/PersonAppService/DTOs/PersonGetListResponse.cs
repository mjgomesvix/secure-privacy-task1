using System;

namespace ApplicationLayer.PersonAppService.DTOs
{
    public class PersonGetListResponse
    {
        internal PersonGetListResponse()
        {
        }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}