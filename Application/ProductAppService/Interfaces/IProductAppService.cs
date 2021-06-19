using ApplicationLayer.Base.Models;
using ApplicationLayer.ProductAppService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.ProductAppService.Interfaces
{
    public interface IProductAppService
    {
        Task<SuccessWithDataResponse<IEnumerable<ProductGetListResponseDto>>> GetListAsync();
        Task<SuccessWithDataResponse<ProductGetAsyncResponseDto>> GetAsync(string id);
        Task<SuccessWithDataResponse<ProductGetAsyncResponseDto>> InsertAsync(ProductInsertRequestDto productInsertRequestDto);
        Task<SuccessWithDataResponse<ProductGetAsyncResponseDto>> UpdateAsync(string id, ProductUpdateRequestDto productUpdateRequestDto);
        Task<SuccessResponse> DeleteAsync(string id);
    }
}
