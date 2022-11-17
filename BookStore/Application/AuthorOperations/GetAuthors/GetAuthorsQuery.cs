using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.GetAuthors
{
    public class GetAuthorsQuery
	{
		private readonly IBookStoreDBContext _context;

		private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorsViewModel> Handle()
        {
            var author = _context.Authors.OrderBy(x => x.Id);
            List<GetAuthorsViewModel> returnObj = _mapper.Map<List<GetAuthorsViewModel>>(author);
            return returnObj;
        }
    }

    public class GetAuthorsViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}

