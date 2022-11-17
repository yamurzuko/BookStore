using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.GetAuthorDetail
{
    public class GetAuthorDetailQuery
	{
        public int AuthorId { get; set; }

        public GetAuthorDetailModel Model { get; set; }

		private readonly IBookStoreDBContext _context;

		private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetAuthorDetailModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if(author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            return _mapper.Map<GetAuthorDetailModel>(author);
        }
    }

    public class GetAuthorDetailModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string BirthDate { get; set; }
    }
}

