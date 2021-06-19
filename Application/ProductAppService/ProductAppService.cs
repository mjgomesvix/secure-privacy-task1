using ApplicationLayer.Base;
using ApplicationLayer.Base.Models;
using ApplicationLayer.ProductAppService.DTOs;
using ApplicationLayer.ProductAppService.Interfaces;
using ApplicationLayer.Resources;
using AutoMapper;
using DomainLayer.Entities.ProductModel;
using DomainLayer.Entities.ProductModel.Commands;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using Support.ExceptionsManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLayer.ProductAppService
{
    public class ProductAppService : ApplicationServiceBase, IProductAppService
    {
        public ProductAppService(IMongoUnitOfWorkFactory uowFactory, IMapper mapper)
            : base(uowFactory, mapper)
        {
        }

        public async Task<SuccessWithDataResponse<ProductGetAsyncResponseDto>> GetAsync(string id)
        {
            using var uow = UowFactory.Create();
            var product = await uow.GetRepository<Product>().GetAsync(id).ConfigureAwait(false);

            if (product == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Product"));

            return new SuccessWithDataResponse<ProductGetAsyncResponseDto>(Mapper.Map<ProductGetAsyncResponseDto>(product), ApplicationMessages.Success);
        }

        public async Task<SuccessWithDataResponse<IEnumerable<ProductGetListResponseDto>>> GetListAsync()
        {
            using var uow = UowFactory.Create();
            var products = await uow.GetRepository<Product>().GetAllAsync().ConfigureAwait(false);
            var products2 = await uow.GetRepository<Product>().GetAllAsync().ConfigureAwait(false);

            //var teste = products.Aggregate(products2, (p2, p) => p2.Where(obj => obj.Id == p.Id));

            var teste = from p in products
                        select p;

            var teste2 = products.Where(p => p.Id != null);

            if (!products.Any())
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Products"));

            return new SuccessWithDataResponse<IEnumerable<ProductGetListResponseDto>>(Mapper.Map<IEnumerable<ProductGetListResponseDto>>(teste2), ApplicationMessages.Success);
        }

        public async Task<SuccessWithDataResponse<ProductGetAsyncResponseDto>> InsertAsync(ProductInsertRequestDto productInsertRequestDto)
        {
            if (productInsertRequestDto == null)
                throw new ArgumentNullException(typeof(ProductInsertRequestDto).Name);

            using var uow = UowFactory.Create();
            var personDomainCreateUpdateCommand = Mapper.Map<ProductDomainCreateUpdateCommand>(productInsertRequestDto);

            var product = personDomainCreateUpdateCommand.Create();

            var productRepository = uow.GetRepository<Product>();

            await productRepository.InsertAsync(product).ConfigureAwait(false);

            await uow.CommitAsync().ConfigureAwait(false);

            var result = await GetAsync(product.Id).ConfigureAwait(false);

            result.Message = ApplicationMessages.Success;

            return result;
        }

        public async Task<SuccessWithDataResponse<ProductGetAsyncResponseDto>> UpdateAsync(string id, ProductUpdateRequestDto productUpdateRequestDto)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (productUpdateRequestDto == null)
                throw new ArgumentNullException(nameof(productUpdateRequestDto));

            using var uow = UowFactory.Create();
            var productRepository = uow.GetRepository<Product>();

            var product = await productRepository.GetAsync(id).ConfigureAwait(false);

            if (product == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, typeof(Product).Name));

            var productDomainCreateUpdateCommand = Mapper.Map<ProductDomainCreateUpdateCommand>(productUpdateRequestDto);

            product.Update(productDomainCreateUpdateCommand);

            await productRepository.UpdateAsync(product).ConfigureAwait(false);

            await uow.CommitAsync().ConfigureAwait(false);

            var result = await GetAsync(product.Id).ConfigureAwait(false);

            result.Message = ApplicationMessages.Success;

            return result;
        }

        public async Task<SuccessResponse> DeleteAsync(string id)
        {
            using var uow = UowFactory.Create();
            var productRepository = uow.GetRepository<Product>();

            var product = await productRepository.GetAsync(id).ConfigureAwait(false);

            if (product == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, typeof(Product).Name));

            await productRepository.DeleteAsync(product).ConfigureAwait(false);
            await uow.CommitAsync().ConfigureAwait(false);

            product = await productRepository.GetAsync(id).ConfigureAwait(false);

            return new SuccessResponse { Message = ApplicationMessages.Success };
        }
    }
}
