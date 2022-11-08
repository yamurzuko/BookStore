﻿using BookStore.DBOperations;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }

        private readonly BookStoreDBContext _dbContect;

        public CreateBookCommand(BookStoreDBContext dBContext)
        {
            _dbContect = dBContext;
        }

        public void Handle()
        {
            var book = _dbContect.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book != null)
            {
                throw new InvalidOperationException("Kitap Zaten Mevcut");
            }

            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            _dbContect.Books.Add(book);
            _dbContect.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }

            public int PageCount { get; set; }

            public DateTime PublishDate { get; set; }
        }
    }
}

