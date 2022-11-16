using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.DeleteAuthor
{
    public class DeleteAuthorCommand
	{
        public int AuthorId { get; set; }

		private readonly BookStoreDBContext _context;

        public DeleteAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}

