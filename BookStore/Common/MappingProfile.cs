using AutoMapper;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.GenreOperations.GetGenreDetail;
using BookStore.Application.GenreOperations.GetGenres;
using BookStore.Entities;
using static BookStore.Application.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.Application.GenreOperations.CreateGenre.CreateGenreCommand;

namespace BookStore.Common
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateBookModel, Book>();
			CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
		}
	}
}

