using LibraryWebAPI.Models;
using LibraryWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/book")]
    public class ReaderController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public ReaderController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            try
            {
                return Ok(await _bookRepository.GetBooks());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byTitle/{title}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByTitle(string title)
        {
            try
            {
                return Ok(await _bookRepository.GetBooksByTitle(title));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byPublisher/{publisher}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByPublisher(string publisher)
        {
            try
            {
                return Ok(await _bookRepository.GetBooksByPublisher(publisher));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byPublisherDate/{date}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByPublisherDate(string date)
        {
            try
            {
                var publisherDate = DateTime.Parse(date);
                return Ok(await _bookRepository.GetBooksByPublishingDate(publisherDate));
            }
            catch (FormatException)
            {
                return BadRequest("Wrong Date format");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byPublisher/{genre}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByGenre(string genre)
        {
            try
            {
                return Ok(await _bookRepository.GetBooksByGenre(genre));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byPublisher/{ISBN}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByISBN(int ISBN)
        {
            try
            {
                return Ok(await _bookRepository.GetBooksByISBN(ISBN));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("byState/{bookStatus}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByISBN(BookStatus bookStatus)
        {
            try
            {
                return Ok(await _bookRepository.GetBooksByStatus(bookStatus));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("ReserveBook/{id}")]
        public async Task<ActionResult<Book>> ReserveBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBook(id);

                if(book == null)
                    return NotFound();

                if (book.bookStatus != BookStatus.Available)
                    return BadRequest("Book is not available");

                var result = await _bookRepository.ModifyBookStatus(id, BookStatus.Reserved);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("BorrowBook/{id}")]
        public async Task<ActionResult<Book>> BorrowBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBook(id);

                if (book == null)
                    return NotFound();

                if (book.bookStatus != BookStatus.Available)
                    return BadRequest("Book is not available");

                var result = await _bookRepository.ModifyBookStatus(id, BookStatus.Borrowed);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
