using DEPI_Session1_WebApi.Infrastructure.Data;
using DEPI_Session1_WebApi.Dtos.Loan;
using DEPI_Session1_WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LibraryDbContext _dbContext;

        public LoansController(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all loans.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .ToListAsync();

            return Ok(loans.Select(ToResponse).ToList());
        }

        /// <summary>
        /// Get one loan by id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await GetLoanAsync(id);
            if (loan is null)
                return NotFound($"Loan with Id = {id} is not found.");

            return Ok(ToResponse(loan));
        }

        /// <summary>
        /// Loan a book to a borrower.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanRequest request)
        {
            if (!await _dbContext.Books.AnyAsync(b => b.Id == request.BookId))
                return BadRequest($"Book with Id = {request.BookId} is not found.");

            if (!await _dbContext.Borrowers.AnyAsync(b => b.Id == request.BorrowerId))
                return BadRequest($"Borrower with Id = {request.BorrowerId} is not found.");

            // the same copy can't be with 2 people at the same time
            if (await _dbContext.Loans.AnyAsync(l => l.BookId == request.BookId && l.ReturnDate == null))
                return BadRequest("This book is already loaned and not returned yet.");

            var loan = new Loan
            {
                BookId = request.BookId,
                BorrowerId = request.BorrowerId,
                LoanDate = request.LoanDate ?? DateTime.UtcNow
            };

            await _dbContext.Loans.AddAsync(loan);
            await _dbContext.SaveChangesAsync();

            var created = await GetLoanAsync(loan.Id);
            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, ToResponse(created!));
        }

        /// <summary>
        /// Update loan dates (send ReturnDate when the book comes back).
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateLoanRequest request)
        {
            if (request.ReturnDate is not null && request.ReturnDate < request.LoanDate)
                return BadRequest("Return date can't be before the loan date.");

            var loan = await GetLoanAsync(request.Id);
            if (loan is null)
                return NotFound($"Loan with Id = {request.Id} is not found.");

            loan.LoanDate = request.LoanDate;
            loan.ReturnDate = request.ReturnDate;

            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();

            return Ok(ToResponse(loan));
        }

        /// <summary>
        /// Delete a loan.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _dbContext.Loans.FirstOrDefaultAsync(l => l.Id == id);
            if (loan is null)
                return NotFound($"Loan with Id = {id} is not found.");

            _dbContext.Loans.Remove(loan);
            await _dbContext.SaveChangesAsync();

            return Ok("Loan deleted successfully!");
        }

        private async Task<Loan?> GetLoanAsync(int id)
            => await _dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .FirstOrDefaultAsync(l => l.Id == id);

        private static LoanResponse ToResponse(Loan loan) => new LoanResponse
        {
            Id = loan.Id,
            BookId = loan.BookId,
            BookTitle = loan.Book.Title,
            BorrowerId = loan.BorrowerId,
            BorrowerName = loan.Borrower.Name,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            IsReturned = loan.ReturnDate != null
        };
    }
}
