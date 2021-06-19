using ApplicationLayer.OrderAppService.DTOs;
using ApplicationLayer.PersonAppService.DTOs;
using ApplicationLayer.ProductAppService.DTOs;
using AutoMapper;
using DomainLayer.Entities.OrderItemModel;
using DomainLayer.Entities.OrderModel;
using DomainLayer.Entities.PersonModel;
using DomainLayer.Entities.PersonModel.Commands;
using DomainLayer.Entities.ProductModel;
using DomainLayer.Entities.ProductModel.Commands;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLayer.Configs.AutoMappers
{
    public static class ApplicationLayerAutoMapper
    {
        public static void GetMap(IProfileExpression cfg)
        {
            cfg
                .CreateMap<IEnumerable<Person>, IEnumerable<PersonGetListResponse>>()
                .ConvertUsing(people => people.ConvertPersonToPersonGetListResponse());

            cfg
                .CreateMap<Person, PersonGetAsyncResponse>()
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(orig => orig.Phones));

            cfg
                .CreateMap<PersonInsertRequest, PersonDomainCreateUpdateCommand>();

            cfg
                .CreateMap<PersonUpdateRequest, PersonDomainCreateUpdateCommand>();

            cfg
                .CreateMap<IEnumerable<Product>, IEnumerable<ProductGetListResponseDto>>()
                .ConvertUsing(products => products.Select(product => product.ConvertProductToProductGetListResponseDto()));

            cfg
                .CreateMap<Product, ProductGetAsyncResponseDto>();

            cfg
                .CreateMap<ProductInsertRequestDto, ProductDomainCreateUpdateCommand>();

            cfg
                .CreateMap<IEnumerable<Order>, IEnumerable<OrderListResponseDto>>()
                .ConvertUsing(order => order.ConvertOrderToOrderListResponseDto());

            cfg
                .CreateMap<Order, OrderResponseDto>();

            cfg
                .CreateMap<OrderItem, OrderItemListResponseDto>();
        }

        private static IEnumerable<OrderListResponseDto> ConvertOrderToOrderListResponseDto(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderListResponseDto
            {
                PersonId = order.PersonId,
                Id = order.Id,
                CreatedAt = order.CreatedAt
            });
        }

        private static ProductGetListResponseDto ConvertProductToProductGetListResponseDto(this Product product)
        {
            return new ProductGetListResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                CreatedAt = product.CreatedAt
            };
        }

        private static IEnumerable<PersonGetListResponse> ConvertPersonToPersonGetListResponse(this IEnumerable<Person> people)
        {
            return people.Select(person => new PersonGetListResponse
            {
                Id = person.Id,
                Name = person.Name,
                Birthday = person.Birthday,
                CreatedAt = person.CreatedAt
            });
        }
    }
}
