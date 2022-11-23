using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IBookStoreDBContext _context;

        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user != null)
            {
                throw new InvalidOperationException("Kullanıcı Zaten Mevcut");
            }

            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

