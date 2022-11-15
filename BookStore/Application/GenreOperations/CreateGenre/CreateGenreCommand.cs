using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.CreateGenre
{
    public class CreateGenreCommand
	{
		public CreateGenreModel Model { get; set; }

		private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if(genre is not null)
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }

            genre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }



		public class CreateGenreModel
		{
			public string Name { get; set; }
		}
	}
}

