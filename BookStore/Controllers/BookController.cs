using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;
using FluentValidation;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery detailQuery = new GetBookDetailQuery(_context, _mapper);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            try
            {
                detailQuery.BookId = id;
                validator.ValidateAndThrow(detailQuery);
                result = detailQuery.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost]

        public IActionResult addBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBook = new CreateBookCommand(_context, _mapper);
            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            try
            {
                createBook.Model = newBook;
                validator.ValidateAndThrow(createBook);
                createBook.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
     
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            try
            {
                command.BookId = id;
                command.Model = updateBook;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBook = new DeleteBookCommand(_context);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            try
            {
                deleteBook.BookId = id;
                validator.ValidateAndThrow(deleteBook);
                deleteBook.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}

