using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
	{
		public int GenreId { get; set; }

		public UpdateGenreModel Model { get; set; }

		private readonly BookStoreDBContext _context;

		private readonly IMapper _mapper;

        public UpdateGenreCommand(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

		public void Handle()
		{
			var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

			if(genre is null)
			{
				throw new InvalidOperationException("Kitap türü bulunamadı.");
            }

            genre.Name = Model.Name != default ? Model.Name : genre.Name;
			genre.IsActive = Model.IsActive;

            _context.SaveChanges();
        }
    }

	public class UpdateGenreModel
	{
        public string Name { get; set; }

		public bool IsActive { get; set; } = true;
	}
}

