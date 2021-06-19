using System.Collections.Generic;

namespace ApplicationLayer.PersonAppService.DTOs
{
    public class PersonGetAsyncResponse : PersonGetListResponse
    {
        internal PersonGetAsyncResponse()
        {
        }
        public IEnumerable<long> Phones { get; set; }
    }
}
