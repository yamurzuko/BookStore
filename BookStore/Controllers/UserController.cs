using AutoMapper;
using BookStore.Application.UserOperations.CreateToken;
using BookStore.Application.UserOperations.CreateUser;
using BookStore.DBOperations;
using BookStore.TokenOperations.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
	{

        private readonly IBookStoreDBContext _context;

        private readonly IMapper _mapper;

        readonly IConfiguration _configuration;

        public UserController(IBookStoreDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]

        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;

            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]

        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;

            var token = command.Handle();
            return token;
        }
    }
}

