using AutoMapper;
using BookStore.Application.BookOperations.CreateBook;
using BookStore.Application.BookOperations.DeleteBook;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookStoreDBContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDBContext context, IMapper mapper)
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
           
            detailQuery.BookId = id;
            validator.ValidateAndThrow(detailQuery);
            result = detailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]

        public IActionResult addBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand createBook = new CreateBookCommand(_context, _mapper);
            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            createBook.Model = newBook;
            validator.ValidateAndThrow(createBook);
            createBook.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
     
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            command.BookId = id;
            command.Model = updateBook;
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBook = new DeleteBookCommand(_context);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            deleteBook.BookId = id;
            validator.ValidateAndThrow(deleteBook);
            deleteBook.Handle();
            
            return Ok();
        }
    }
}

