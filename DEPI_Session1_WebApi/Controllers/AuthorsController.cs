using DEPI_Session1_WebApi.Infrastructure.Data;
using DEPI_Session1_WebApi.Dtos.Author;
using DEPI_Session1_WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorsController(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all authors.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _dbContext.Authors
                .Include(a => a.Books)
                .ToListAsync();

            var response = authors.Select(ToResponse).ToList();
            return Ok(response);
        }

        /// <summary>
        /// Get one author by id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _dbContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                return NotFound($"Author with Id = {id} is not found.");

            return Ok(ToResponse(author));
        }

        /// <summary>
        /// Add a new author.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorRequest request)
        {
            var author = new Author
            {
                Name = request.Name,
                BirthDate = request.BirthDate
            };

            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = author.Id }, ToResponse(author));
        }

        /// <summary>
        /// Update an author.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuthorRequest request)
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (author is null)
                return NotFound($"Author with Id = {request.Id} is not found.");

            author.Name = request.Name;
            author.BirthDate = request.BirthDate;

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return Ok(ToResponse(author));
        }

        /// <summary>
        /// Delete an author (not allowed if he has books).
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author is null)
                return NotFound($"Author with Id = {id} is not found.");

            if (await _dbContext.Books.AnyAsync(b => b.AuthorId == id))
                return BadRequest("Can't delete this author because he has books.");

            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();

            return Ok("Author deleted successfully!");
        }

        private static AuthorResponse ToResponse(Author author) => new AuthorResponse
        {
            Id = author.Id,
            Name = author.Name,
            BirthDate = author.BirthDate,
            BooksCount = author.Books.Count,
            BookTitles = author.Books.Select(b => b.Title).ToList()
        };
    }
}
