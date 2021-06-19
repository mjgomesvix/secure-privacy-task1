using ApplicationLayer.Base.Models;
using ApplicationLayer.OrderAppService.DTOs;
using ApplicationLayer.OrderAppService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;
        public SalesController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        /// <summary>
        /// Gets a list of Order.
        /// </summary>
        /// <param name="orderListRequestDto">Filter</param>
        [HttpGet]
        [Route("orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<IEnumerable<OrderListResponseDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<IEnumerable<OrderListResponseDto>>>> GetOrdersAsync([FromQuery] OrderListRequestDto orderListRequestDto)
        {
            return Ok(await _orderAppService.GetListAsync(orderListRequestDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets a specific Order.
        /// </summary>
        /// <param name="id">Order Id</param>
        [HttpGet]
        [Route("orders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<OrderResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<OrderResponseDto>>> GetOrderAsync(string id)
        {
            return Ok(await _orderAppService.GetAsync(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Inserts a Order.
        /// </summary>
        /// <param name="orderInsertRequestDto">Order insert object</param>
        [HttpPost]
        [Route("orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<OrderResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<OrderResponseDto>>> InsertOrderAsync(OrderInsertRequestDto orderInsertRequestDto)
        {
            return Ok(await _orderAppService.InsertAsync(orderInsertRequestDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Add item to the specific Order.
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="orderItemAddRequest">Order Item add object</param>
        [HttpPost]
        [Route("orders/{id}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessResponse>> AddItemToOrderAsync(string id, OrderItemAddRequest orderItemAddRequest)
        {
            return Ok(await _orderAppService.AddItemToOrderAsync(id, orderItemAddRequest).ConfigureAwait(false));
        }

        /// <summary>
        /// Remove a specific item from the Order.
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="itemId">Order Item Id</param>
        [HttpDelete]
        [Route("orders/{id}/items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessResponse>> RemoveItemFromOrderAsync(string id, string itemId)
        {
            return Ok(await _orderAppService.RemoveItemFromOrderAsync(id, itemId).ConfigureAwait(false));
        }
    }
}
