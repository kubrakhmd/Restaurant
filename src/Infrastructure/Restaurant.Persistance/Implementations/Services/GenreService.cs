using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;

namespace Restaurant.Persistence.Implementations.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GenreItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Genre> genres = await _repository
                .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();
            return _mapper.Map<IEnumerable<GenreItemDto>>(genres);
        }

        public async Task<GetGenreDto> GetByIdAsync(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id, nameof(Genre.Blogs));

            if (genre is null) throw new Exception(" Genre is not exist");

            return _mapper.Map<GetGenreDto>(genre);
        }

        public async Task CreateAsync(CreateGenreDto genreDto)
        {
            if (await _repository.AnyAsync(c => c.Name == genreDto.Name)) throw new Exception("already exists");
            Genre genre = _mapper.Map<Genre>(genreDto);

            genre.CreatedAt = DateTime.Now;
           
            await _repository.AddAsync(genre);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateGenreDto genreDto)
        {
            Genre genre = await _repository.GetByIdAsync(id);
            if (genre == null) throw new Exception("Not found");
            if (await _repository.AnyAsync(c => c.Name == genreDto.Name && c.Id != id)) throw new Exception("Already exists");

            genre = _mapper.Map(genreDto, genre);

            genre.Name = genreDto.Name;

            _repository.Update(genre);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id);
            if (genre == null) throw new Exception("Not found");
            _repository.Delete(genre);
            await _repository.SaveChangesAsync();
        }


        public async Task SoftDelete(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id);
            if (genre == null) throw new Exception("Not found");
            genre.IsDeleted = true;
            _repository.Update(genre);
            await _repository.SaveChangesAsync();
        }

    }



}



