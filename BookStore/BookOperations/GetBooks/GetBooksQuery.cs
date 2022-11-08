using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _dBContext;

        public GetBooksQuery(BookStoreDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dBContext.Books.OrderBy(x => x.Id).ToList<Book>();
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
            return viewModel;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}

