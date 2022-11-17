using AutoMapper;
using BookStore.Application.AuthorOperations.CreateAuthor;
using BookStore.Application.AuthorOperations.DeleteAuthor;
using BookStore.Application.AuthorOperations.GetAuthorDetail;
using BookStore.Application.AuthorOperations.GetAuthors;
using BookStore.Application.AuthorOperations.UpdateAuthor;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDBContext _context;

        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]

        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();

            query.AuthorId = id;
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]

        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

            command.Model = newAuthor;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]

        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            command.AuthorId = id;
            command.Model = updateAuthor;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }

        [HttpDelete("id")]

        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            command.AuthorId = id;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }
    }
}


