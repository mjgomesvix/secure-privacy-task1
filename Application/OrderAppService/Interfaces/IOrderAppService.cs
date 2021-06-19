using ApplicationLayer.Base.Models;
using ApplicationLayer.OrderAppService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.OrderAppService.Interfaces
{
    public interface IOrderAppService
    {
        Task<SuccessWithDataResponse<IEnumerable<OrderListResponseDto>>> GetListAsync(OrderListRequestDto orderListRequestDto);
        Task<SuccessWithDataResponse<OrderResponseDto>> GetAsync(string id);
        Task<SuccessWithDataResponse<OrderResponseDto>> InsertAsync(OrderInsertRequestDto orderInsertRequestDto);
        Task<SuccessResponse> AddItemToOrderAsync(string orderId, OrderItemAddRequest orderItemAddRequest);
        Task<SuccessResponse> RemoveItemFromOrderAsync(string id, string itemId);
    }
}
