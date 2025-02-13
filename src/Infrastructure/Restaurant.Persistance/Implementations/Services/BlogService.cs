
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;
using Restaurant.Domain.Models;


namespace Restaurant.Persistence.Implementations.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Blog> blogs = await _repository
                .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<BlogItemDto>>(blogs);
        }

        public async Task<GetBlogDto> GetByIdAsync(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id, nameof(Blog.Author), nameof(Blog.Genre));
            if (blog == null) throw new Exception("Not Found");
       

            return _mapper.Map<GetBlogDto>(blog);
        }
          
        public async Task CreateAsync(CreateBlogDto blogDto)
        {
            var tagEntities = await _repository.GetManyToManyEntities<Tag>(blogDto.TagIds);
            if (tagEntities.Count() != blogDto.TagIds.Distinct().Count())
                throw new Exception("Tag id is wrong");
            Blog blog = _mapper.Map<Blog>(blogDto);

            blog.CreatedAt = DateTime.Now;
           
            await _repository.AddAsync(blog);
            await _repository.SaveChangesAsync();
        }
  
        public async Task UpdateAsync(int id, UpdateBlogDto blogDto)
        {
           
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog == null) throw new Exception("Not found");
            if (await _repository.AnyAsync(c => c.Id != id)) throw new Exception("Already exists");
            ICollection<int> createItems3 = blogDto.TagIds
             .Where(cId => !blog.BlogTags.Any(pt => pt.TagId == cId)).ToList();
            var tagEntities = await _repository.GetManyToManyEntities<Tag>(createItems3);
            if (tagEntities.Count() != createItems3.Distinct().Count())
                throw new Exception("One or more tag id is wrong");
            blog = _mapper.Map(blogDto, blog);
            blog.Article = blogDto.Article;
            blog.Image = blogDto.Image;
            blog.AuthorId = blogDto.AuthorId;
            blog.GenreId = blogDto.GenreId;


            _repository.Update(blog);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Blog blog = await _repository.GetByIdAsync(Id);
            if (blog == null) throw new Exception("Not Found");

            _repository.Delete(blog);

            await _repository.SaveChangesAsync();
        }
        public async Task SoftDelete(int id)
        {
            Blog blog = await _repository.GetByIdAsync(id);
            if (blog == null) throw new Exception("Not found");
            blog.IsDeleted = true;
            _repository.Update(blog);
            await _repository.SaveChangesAsync();
        }
    }
}