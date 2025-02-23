using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurant.Application.DTOs.AccountDto;
using Restaurant.Application.DTOs.ReservationDto;
using Restaurant.Application.DTOs.TableDto;
using Restaurant.Domain.Models;

namespace Restaurant.Application.MappingProfiles
{
    internal class RezervationProfile:Profile
        
    {
        public RezervationProfile()
        {
            CreateMap<TableDto, RestaurantTable>();

            CreateMap<Rezervation, ReservationDto>()
           .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => new UserInfoDto
           {
               Id = src.User.Id,
               UserName = src.User.UserName,
               Email = src.User.Email
           }));

            CreateMap<Rezervation, CreateReservationDto>().ReverseMap();
            CreateMap<Rezervation, UpdateReservationDto>().ReverseMap();
        }

    }
    }

