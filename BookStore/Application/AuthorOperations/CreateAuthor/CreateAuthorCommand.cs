using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.CreateAuthor
{
	public class CreateAuthorCommand
	{
        public CreateAuthorModel Model { get; set; }

		private readonly BookStoreDBContext _context;

		private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name);

            if(author != null)
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }

            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }
    }
}

