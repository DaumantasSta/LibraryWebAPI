using LibraryWebAPI.Models;
using LibraryWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/book")]
    public class ManagerController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public ManagerController(IBookRepository bookRepository)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            try
            {
                var result = await _bookRepository.GetBook(id);

                if (result == null)
                    return NotFound($"Book with Id = {id} not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest();

                var result = await _bookRepository.AddBook(book);

                if (result == null)
                    return BadRequest();

                return Created(result.Id.ToString(), result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, [FromBody] Book book)
        {
            try
            {
                var result = await _bookRepository.UpdateBook(id, book);

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

        [HttpPut("RegisterBookReturn/{id}")]
        public async Task<ActionResult<Book>> RegisterBookReturn(int id)
        {
            try
            {
                var result = await _bookRepository.ModifyBookStatus(id, BookStatus.Available);

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _bookRepository.DeleteBook(id);

                return Accepted();
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
