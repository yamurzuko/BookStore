using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }

        public readonly IBookStoreDBContext _context;

        public readonly IMapper _mapper;

        public GetGenreDetailQuery(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);

            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

            return _mapper.Map<GenreDetailViewModel>(genre);
             
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

