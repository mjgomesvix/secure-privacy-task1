using ApplicationLayer.Base.Models;
using ApplicationLayer.PersonAppService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.PersonAppService.Interfaces
{
    public interface IPersonAppService
    {
        Task<SuccessWithDataResponse<IEnumerable<PersonGetListResponse>>> GetListAsync();
        Task<SuccessWithDataResponse<PersonGetAsyncResponse>> GetAsync(string id);
        Task<SuccessWithDataResponse<PersonGetAsyncResponse>> InsertAsync(PersonInsertRequest personInsertRequest);
        Task<SuccessWithDataResponse<PersonGetAsyncResponse>> UpdateAsync(string id, PersonUpdateRequest personUpdateRequest);
        Task<SuccessResponse> DeleteAsync(string id);
    }
}
