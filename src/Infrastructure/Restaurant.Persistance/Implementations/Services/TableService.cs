using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Restaurant.Application.DTOs.TableDto;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Servises
{
    public class TableService : ITableService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public TableService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }
        public async Task GetAllTables()
        {
            try
            {
                var tables = await _context.Tables.ToListAsync();

                if (tables == null)
                {
                    throw new Exception("Tables not found");
                }

              
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
        public async Task GetTableById(int id)
        {
            try
            {
                var table = await _context.Tables.FindAsync(id);

                if (table == null)
                {
                    throw new Exception("Table not found");
                }

              
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
        public async Task CreateTable(TableDto tableDto)
        {
            if (tableDto == null)
            {
                throw new Exception("Table is required");   
            }

            try
            {
                var table = new RestaurantTable()
                {
                    Capacity = tableDto.Capacity,
                    TableNumber = tableDto.TableNumber,
                };
                _context.Tables.Add(table);
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
        public async Task UpdateTable(int id, TableDto tableDto)
        {
            try
            {
                var table = await _context.Tables.FindAsync(id);

                if (table == null)
                    throw new Exception("Table not found"); 


                _mapper.Map(tableDto, table);
                table.UpdatedAt = DateTime.UtcNow;
                _context.Tables.Update(table);
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
        public async Task DeleteTable(int id)
        {
            try
            {
                var table = await _context.Tables.FindAsync(id);

                if (table == null)
                {
                    throw new Exception("Table not found");
                }

                _context.Tables.Remove(table);
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
    }
}
