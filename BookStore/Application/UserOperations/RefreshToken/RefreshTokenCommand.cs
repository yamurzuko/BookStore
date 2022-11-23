using BookStore.DBOperations;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;

namespace BookStore.Application.UserOperations.RefreshToken
{
    public class RefreshTokenCommand
    {
        public  string refreshToken { get; set; }

        private readonly IBookStoreDBContext _context;

        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken && x.RefreshTokenExpireDate > DateTime.Now);

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
                throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı.");
            }

        }
    }
}

