using LibraryWebAPI.Models;
using LibraryWebAPI.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly ApiDbContext _apiDbContext;

        public BookRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
            _apiDbContext.Database.EnsureCreated();
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _apiDbContext.Book.ToListAsync();
        }

        public async Task<Book> GetBook(int bookId)
        {
            return await _apiDbContext.Book.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooksByTitle(string title)
        {
            return await _apiDbContext.Book.Where(x => x.Title == title).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByPublisher(string publisher)
        {
            return await _apiDbContext.Book.Where(x => x.Publisher == publisher).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByPublishingDate(DateTime publisherDate)
        {
            return await _apiDbContext.Book.Where(x => x.PublishDate == publisherDate).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(string genre)
        {
            return await _apiDbContext.Book.Where(x => x.Genre == genre).ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByISBN(int ISBN)
        {
            return await _apiDbContext.Book.Where(x => x.ISBN == ISBN).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByStatus(BookStatus bookStatus)
        {
            return await _apiDbContext.Book.Where(x => x.bookStatus == bookStatus).ToListAsync();
        }

        public async Task<Book> AddBook(Book book)
        {
            var result = await _apiDbContext.Book.AddAsync(book);
            await _apiDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Book> UpdateBook(int id, Book book)
        {
            var bookExist = await _apiDbContext.Book.AnyAsync(x => x.Id == id);

            if (!bookExist)
                throw new KeyNotFoundException($"Book with ID = {id} not found");

            book.Id = id;
            _apiDbContext.Book.Update(book);
            await _apiDbContext.SaveChangesAsync();

            return book;
        }

        public async Task<Book> ModifyBookStatus(int id, BookStatus bookStatus)
        {
            var book =  await _apiDbContext.Book.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID = {id} not found");
            
            book.bookStatus = bookStatus;
            book = await UpdateBook(id, book);

            return book;
        }

        public async Task DeleteBook(int id)
        {
            var bookToDelete = await GetBook(id);

            if (bookToDelete == null)
                throw new KeyNotFoundException($"Book with Id = {id} not found");

            _apiDbContext.Book.Remove(bookToDelete);
            await _apiDbContext.SaveChangesAsync();
        }
    }
}
