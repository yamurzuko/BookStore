﻿using AutoMapper;
using BookStore.Application.BookOperations.CreateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;
using static BookStore.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Tests.Applications.BookOperationTests
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange = Hazırlık

            var book = new Book()
            {
                Title = "Tests_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990, 04, 04),
                GenreId = 1,
                AuthorId = 1
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            //act = Çalıştırma
            //assert = Doğrulama

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut");

        }

        [Fact]

        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange = Hazırlık

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = new System.DateTime(1990, 04, 04),
                GenreId = 1,
                AuthorId = 1
            };

            command.Model = model;

            //act = Çalıştırma
            //assert = Doğrulama

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);

        }
    }
}

