using ApplicationLayer.Base;
using ApplicationLayer.Base.Models;
using ApplicationLayer.OrderAppService.DTOs;
using ApplicationLayer.OrderAppService.Interfaces;
using ApplicationLayer.Resources;
using AutoMapper;
using DomainLayer.Entities.OrderItemModel;
using DomainLayer.Entities.OrderItemModel.Commands;
using DomainLayer.Entities.OrderModel;
using DomainLayer.Entities.OrderModel.Commands;
using DomainLayer.Entities.PersonModel;
using DomainLayer.Entities.ProductModel;
using LinqKit;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using Support.ExceptionsManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationLayer.OrderAppService
{
    public class OrderAppService : ApplicationServiceBase, IOrderAppService
    {
        public OrderAppService(IMongoUnitOfWorkFactory uowFactory, IMapper mapper)
            : base(uowFactory, mapper)
        {
        }

        public async Task<SuccessResponse> AddItemToOrderAsync(string orderId, OrderItemAddRequest orderItemAddRequest)
        {
            if (string.IsNullOrEmpty(orderId))
                throw new ArgumentNullException(nameof(orderId));

            if (orderItemAddRequest == null)
                throw new ArgumentNullException(nameof(orderItemAddRequest));

            using var uow = UowFactory.Create();

            var order = await uow.GetRepository<Order>().GetAsync(orderId).ConfigureAwait(false);
            if (order == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Order"));

            var product = await uow.GetRepository<Product>().GetAsync(orderItemAddRequest.ProductId).ConfigureAwait(false);
            if (product == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Product"));

            var orderItemDomainCreateCommand = new OrderItemDomainCreateCommand
            {
                Order = order,
                Product = product,
                Quantity = orderItemAddRequest.Quantity,
                UnityPrice = orderItemAddRequest.UnityPrice
            };

            var orderItem = orderItemDomainCreateCommand.Create();

            await uow.GetRepository<OrderItem>().InsertAsync(orderItem).ConfigureAwait(false);
            await uow.CommitAsync().ConfigureAwait(false);

            return new SuccessResponse { Message = ApplicationMessages.Success };
        }

        public async Task<SuccessResponse> RemoveItemFromOrderAsync(string id, string itemId)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException(nameof(itemId));

            using var uow = UowFactory.Create();

            var orderItemRepository = uow.GetRepository<OrderItem>();

            var orderItem = await orderItemRepository.GetAsync(itemId).ConfigureAwait(false);

            if (orderItem == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Item"));

            await orderItemRepository.DeleteAsync(orderItem).ConfigureAwait(false);

            await uow.CommitAsync().ConfigureAwait(false);

            return new SuccessResponse { Message = ApplicationMessages.Success };
        }

        public async Task<SuccessWithDataResponse<IEnumerable<OrderListResponseDto>>> GetListAsync(OrderListRequestDto orderListRequestDto)
        {
            Expression<Func<Order, bool>> expression = PredicateBuilder.New<Order>(false);
            expression = _ => true;

            if (orderListRequestDto != null)
            {
                if (!string.IsNullOrEmpty(orderListRequestDto.PersonId))
                    expression = expression.And(o => o.PersonId == orderListRequestDto.PersonId);

                if (orderListRequestDto.CreatedAt.HasValue)
                {
                    var currentDayStart = orderListRequestDto.CreatedAt.Value;
                    var currentDayEnds = currentDayStart.AddDays(1);

                    expression = expression.And(o => o.CreatedAt >= currentDayStart && o.CreatedAt < currentDayEnds);
                }
            }

            using var uow = UowFactory.Create();
            var orders = await uow.GetRepository<Order>().GetAllAsync(expression).ConfigureAwait(false);

            return new SuccessWithDataResponse<IEnumerable<OrderListResponseDto>>(Mapper.Map<IEnumerable<OrderListResponseDto>>(orders), ApplicationMessages.Success);
        }

        public async Task<SuccessWithDataResponse<OrderResponseDto>> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            using var uow = UowFactory.Create();
            var order = await uow.GetRepository<Order>().GetAsync(id).ConfigureAwait(false);

            if (order == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, typeof(Order).Name));

            var orderItems = await uow.GetRepository<OrderItem>().GetAllAsync(oi => oi.OrderId == order.Id).ConfigureAwait(false);

            var orderResponseDto = Mapper.Map<OrderResponseDto>(order);
            orderResponseDto.Items = orderItems.AsEnumerable().Select(oi =>
            {
                var orderItemListResponseDto = Mapper.Map<OrderItemListResponseDto>(oi);
                orderItemListResponseDto.ProductName = uow.GetRepository<Product>().GetAsync(oi.ProductId).Result.Name;

                return orderItemListResponseDto;
            });

            return new SuccessWithDataResponse<OrderResponseDto>(orderResponseDto, ApplicationMessages.Success);
        }

        public async Task<SuccessWithDataResponse<OrderResponseDto>> InsertAsync(OrderInsertRequestDto orderInsertRequestDto)
        {
            if (orderInsertRequestDto == null)
                throw new ArgumentNullException(typeof(OrderResponseDto).Name);

            using var uow = UowFactory.Create();
            var person = await uow.GetRepository<Person>().GetAsync(orderInsertRequestDto.PersonId).ConfigureAwait(false);

            if (person == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, typeof(Person).Name));

            var orderDomainCreateCommand = new OrderDomainCreateCommand
            {
                Person = person,
                Items = orderInsertRequestDto
                    .Items
                    .Select(async i => new OrderItemDomainAddCommand
                    {
                        Product = await uow.GetRepository<Product>().GetAsync(i.ProductId).ConfigureAwait(false),
                        Quantity = i.Quantity,
                        UnityPrice = i.UnityPrice
                    })
                    .Select(t => t.Result)
            };

            var order = orderDomainCreateCommand.Create();

            var task1 = uow.GetRepository<Order>().InsertAsync(order);
            var task2 = uow.GetRepository<OrderItem>().InsertAsync(order.Items);

            await Task.WhenAll(task1, task2).ConfigureAwait(false);

            await uow.CommitAsync().ConfigureAwait(false);

            var orderGetAsyncResponseDto = Mapper.Map<OrderResponseDto>(order);
            orderGetAsyncResponseDto.Items = Mapper.Map<IEnumerable<OrderItemListResponseDto>>(order.Items);

            return new SuccessWithDataResponse<OrderResponseDto>(orderGetAsyncResponseDto, ApplicationMessages.Success);
        }
    }
}
