using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Application.UserOperations.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }

        private readonly IBookStoreDBContext _context;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if (user != null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Hatalı Giriş");
            }

        }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

