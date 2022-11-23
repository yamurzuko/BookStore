using AutoMapper;
using BookStore.Application.GenreOperations.CreateGenre;
using BookStore.Application.GenreOperations.DeleteGenre;
using BookStore.Application.GenreOperations.GetGenreDetail;
using BookStore.Application.GenreOperations.GetGenres;
using BookStore.Application.GenreOperations.UpdateGenre;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.GenreOperations.CreateGenre.CreateGenreCommand;

namespace BookStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class GenreController : ControllerBase
	{
        private readonly IBookStoreDBContext _context;

        private readonly IMapper _mapper;

        public GenreController(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]

        public IActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

            query.GenreId = id;
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]

        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            command.Model = newGenre;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]

        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            command.GenreId = id;
            command.Model = updateGenre;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }

        [HttpDelete("id")]

        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            command.GenreId = id;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }
	}
}


