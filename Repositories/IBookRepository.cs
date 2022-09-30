using LibraryWebAPI.Models;

namespace LibraryWebAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int bookId);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(int id, Book book);
        Task<Book> ModifyBookStatus(int id, BookStatus bookStatus);
        Task DeleteBook(int id);
        Task<IEnumerable<Book>> GetBooksByTitle(string title);
        Task<IEnumerable<Book>> GetBooksByPublisher(string publisher);
        Task<IEnumerable<Book>> GetBooksByPublishingDate(DateTime publisherDate);
        Task<IEnumerable<Book>> GetBooksByGenre(string genre);
        Task<IEnumerable<Book>> GetBooksByISBN(int ISBN);
        Task<IEnumerable<Book>> GetBooksByStatus(BookStatus bookStatus);

    }
}
