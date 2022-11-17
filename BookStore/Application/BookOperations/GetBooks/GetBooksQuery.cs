using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDBContext _dBContext;

        private readonly IMapper _dbMapper;

        public GetBooksQuery(IBookStoreDBContext dbContext, IMapper dbMapper)
        {
            _dBContext = dbContext;
            _dbMapper = dbMapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dBContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList<Book>();
            /*
            List<BooksViewModel> viewModel = new List<BooksViewModel>();

            foreach(var item in bookList)
            {
                viewModel.Add(new BooksViewModel()
                {
                    Title = item.Title,
                    Genre = ((GenreEnum)item.GenreId).ToString(),
                    PageCount = item.PageCount,
                    PublishDate = item.PublishDate.Date.ToString("dd/mm/yyy"),
                });
            }
            */

            List<BooksViewModel> viewModel = _dbMapper.Map<List<BooksViewModel>>(bookList);
            return viewModel;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}

