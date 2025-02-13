

using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;
using Restaurant.Application.DTOs.Menu;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Implementations.Services
{
public  class MenuService : IMenuService
    {

            private readonly AppDbContext _context;
          
            private readonly IMapper _mapper;
        private readonly IMenuRepository _repository;
        public MenuService(AppDbContext context, IMapper mapper, IMenuRepository repository)
            {
                _context = context;
            _repository = repository;
            _mapper = mapper;
            }
        public async Task<IEnumerable<MenuItem>> GetAllMenuItem(int page, int take)
        {
            IEnumerable<Menu> menus = await _repository
              .GetAll(skip: (page - 1) * take, take: take)
               .ToListAsync();
            return _mapper.Map<IEnumerable<MenuItem>>(menus);
        }
           
            public async Task<MenuItem> GetMenuItemById(int id)
            {
                try
                {
                    var menuItem = await _context.Menus.FindAsync(id);

                    if (menuItem == null) throw new Exception("Menu is not exists");

               return _mapper.Map<MenuItem>(menuItem);
            }
                catch (DbUpdateException ex)
                {
                 throw new Exception (ex.ToString());
                }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
        public async Task<MenuItem> GetMenuItemByName(string name)
            {
                if (string.IsNullOrWhiteSpace(name)) throw new Exception("Name is required");

            try
                {
                    var menuItem = await _context.Menus.FirstOrDefaultAsync(mi => mi.Name == name);

                    if (menuItem == null) throw new Exception("Menu is not exists");



                 return _mapper.Map<MenuItem>(menuItem);
            }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task CreateMenuItem(MenuItem menuItemDto)
            {
                if (menuItemDto == null || string.IsNullOrWhiteSpace(menuItemDto.Name) || string.IsNullOrWhiteSpace(menuItemDto.Description) || menuItemDto.Price <= 0)
               throw new Exception("Data is required"); 
           
                      Menu menu  = _mapper.Map<Menu>(menuItemDto );
                    await _repository.AddAsync(menu);
                    await _repository.SaveChangesAsync();
               
            }
            public async Task UpdateMenuItem(int id, MenuItem menuItemDto)
            {
                if (menuItemDto == null || string.IsNullOrWhiteSpace(menuItemDto.Name) || string.IsNullOrWhiteSpace(menuItemDto.Description) || menuItemDto.Price <= 0)
                throw new Exception("Data is required");

            try
                {
                    var menuItem = await _context.Menus.FindAsync(id);
                    if (menuItem == null)
                  throw new Exception("Menu is not exists");

                    _mapper.Map(menuItemDto, menuItem);
                    menuItem.UpdatedAt = DateTime.UtcNow;
                    _context.Menus.Update(menuItem);
                    await _context.SaveChangesAsync();

                 
                }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }
            public async Task  DeleteMenuItem(int id)
            {
                try
                {
               Menu menu  = await _repository.GetByIdAsync(id);
                if (menu == null) throw new Exception("Not found");
                _repository.Delete(menu);
                await _repository.SaveChangesAsync();
            }
                catch (DbUpdateException ex)
                {
                throw new Exception(ex.ToString());
            }
                catch (Exception ex)
                {
                throw new Exception(ex.ToString());
            }
            }

      
    }

    }


