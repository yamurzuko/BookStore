﻿using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommand
	{
		public int GenreId { get; set; }

		private readonly BookStoreDBContext _context;

        public DeleteGenreCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}

