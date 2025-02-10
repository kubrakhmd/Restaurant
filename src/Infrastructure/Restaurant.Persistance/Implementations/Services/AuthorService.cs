
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;

namespace Restaurant.Persistence.Implementations.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Author> authors = await _repository
                .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<AuthorItemDto>>(authors);
        }

        public async Task<GetAuthorDto> GetByIdAsync(int id)
        {
            Author author = await _repository.GetByIdAsync(id, nameof(Author.Blogs));
            if (author == null) throw new Exception("Not Found");


            return _mapper.Map<GetAuthorDto>(author);
        }

        public async Task CreateAsync(CreateAuthorDto authorDto)
        {

            Author author = _mapper.Map<Author>(authorDto);



            await _repository.AddAsync(author);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateAuthorDto authorDto)
        {
            Author author = await _repository.GetByIdAsync(id);
            if (author == null) throw new Exception("Not found");
            if (await _repository.AnyAsync(c => c.Name == authorDto.Name && c.Id != id)) throw new Exception("Already exists");

            author = _mapper.Map(authorDto, author);

            author.Name = authorDto.Name;
            author.Surname = authorDto.Surname;



            _repository.Update(author);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            Author author = await _repository.GetByIdAsync(id);
            if (author == null) throw new Exception("Not found");
            author.IsDeleted = true;
            _repository.Update(author);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Author author = await _repository.GetByIdAsync(Id);
            if (author == null) throw new Exception("Not Found");

            _repository.Delete(author);

            await _repository.SaveChangesAsync();
        }


    }
}

