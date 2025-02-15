
using AutoMapper;
using Restaurant.Application.DTOs.AccountDto;
using Restaurant.Application.DTOs.OrderDto;
using Restaurant.Domain.Models;

namespace Restaurant.Application.MappingProfiles
{
    internal class OrderProfile:Profile
    {
        public OrderProfile()
        {

            CreateMap<OrderDto, Order>()
                .ForMember(a => a.OrderItems, b => b.Ignore())
                .ForMember(a => a.TotalAmount, b => b.Ignore());
            CreateMap<OrderItemDto, OrderItem>();


            CreateMap<Order, ShowOrder>()
            .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => new UserInfoDto
            {
                Id = src.User.Id,
                UserName = src.User.UserName,
                Email = src.User.Email
            }))
            .ForMember(dest => dest.ShowOrderItem, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItem, ShowOrderItem>();


            CreateMap<User, UserDto>()
                .ForMember(a => a.Token, b => b.Ignore());

        }
    }
}
