using ApplicationLayer.Base.Models;
using ApplicationLayer.PersonAppService.DTOs;
using ApplicationLayer.PersonAppService.Interfaces;
using ApplicationLayer.ProductAppService.DTOs;
using ApplicationLayer.ProductAppService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IPersonAppService _personAppService;
        private readonly IProductAppService _productAppService;

        public RegisterController(IPersonAppService personAppService, IProductAppService productAppService)
        {
            _personAppService = personAppService;
            _productAppService = productAppService;
        }

        /// <summary>
        /// Gets a list of Person.
        /// </summary>
        [HttpGet]
        [Route("people")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<IEnumerable<PersonGetListResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<IEnumerable<PersonGetListResponse>>>> GetPeopleAsync()
        {
            return Ok(await _personAppService.GetListAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Gets a specific Person.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("people/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<PersonGetAsyncResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<PersonGetAsyncResponse>>> GetPersonAsync(string id) => Ok(await _personAppService.GetAsync(id).ConfigureAwait(false));

        /// <summary>
        /// Inserts a Person.
        /// </summary>
        /// <param name="personInsertRequest"></param>
        [HttpPost]
        [Route("people")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<PersonGetAsyncResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<PersonGetAsyncResponse>>> InsertPersonAsync(PersonInsertRequest personInsertRequest)
        {
            return Ok(await _personAppService.InsertAsync(personInsertRequest).ConfigureAwait(false));
        }

        /// <summary>
        /// Updates a specific Person.
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <param name="personUpdateRequest">Person update request object</param>
        [HttpPut]
        [Route("people/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<PersonGetAsyncResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<PersonGetAsyncResponse>>> UpdatePersonAsync(string id, PersonUpdateRequest personUpdateRequest)
        {
            return Ok(await _personAppService.UpdateAsync(id, personUpdateRequest).ConfigureAwait(false));
        }

        /// <summary>
        /// Deletes a specific Person.
        /// </summary>
        /// <param name="id">Person Id</param>
        [HttpDelete]
        [Route("people/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessResponse>> DeletePersonAsync(string id)
        {
            return Ok(await _personAppService.DeleteAsync(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets a list of Products.
        /// </summary>
        [HttpGet]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<IEnumerable<ProductGetListResponseDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<IEnumerable<ProductGetListResponseDto>>>> GetProductsAsync()
        {
            return Ok(await _productAppService.GetListAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Gets a specific Product.
        /// </summary>
        /// <param name="id">Product Id</param>
        [HttpGet]
        [Route("products/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<ProductGetAsyncResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<ProductGetAsyncResponseDto>>> GetProductAsync(string id)
        {
            return Ok(await _productAppService.GetAsync(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Inserts a Product.
        /// </summary>
        /// <param name="productInsertRequest">Product insert request object</param>
        [HttpPost]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<ProductGetAsyncResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<ProductGetAsyncResponseDto>>> InsertProductAsync(ProductInsertRequestDto productInsertRequest)
        {
            return Ok(await _productAppService.InsertAsync(productInsertRequest).ConfigureAwait(false));
        }
        /// <summary>
        /// Updates a specific Product.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="productUpdateRequestDto">Product update request object</param>
        [HttpPut]
        [Route("products/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessWithDataResponse<ProductGetAsyncResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessWithDataResponse<ProductGetAsyncResponseDto>>> UpdateProductAsync(string id, ProductUpdateRequestDto productUpdateRequestDto)
        {
            return Ok(await _productAppService.UpdateAsync(id, productUpdateRequestDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Deletes a specific Product.
        /// </summary>
        /// <param name="id">Product Id</param>
        [HttpDelete]
        [Route("products/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<SuccessResponse>> DeleteProductAsync(string id)
        {
            return Ok(await _productAppService.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}
