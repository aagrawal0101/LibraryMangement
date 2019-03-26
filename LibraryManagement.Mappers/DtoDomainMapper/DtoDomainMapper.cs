using AutoMapper;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Mappers
{
    public static class DtoDomainMapper
    {
        private static IMapper mapper;

        public static IMapper ConfigMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<BookDto, BookDomain>()
                .ForMember(dest => dest.BookId, m => m.MapFrom(src => src.pkBookId))
                .ForMember(dest => dest.BookName, m => m.MapFrom(src => src.BookName))
                .ForMember(dest => dest.BookType, m => m.MapFrom(src => src.BookType))
                .ForMember(dest => dest.BookAuthor, m => m.MapFrom(src => src.BookAuthor))
                .ReverseMap();

            });
            return mapper = config.CreateMapper();
        }
        public static BookDomain MapDtoToDomain(AddNewBookRequest newBookRequest)
        {
            return mapper.Map<BookDomain>(newBookRequest.NewBookDetail);

        }

        public static BookDomain MapDtoToDomainToUpdateBook(UpdateNewBookRequest updateNewBook)
        {
            return mapper.Map<BookDomain>(updateNewBook.UpdateBookDetail);
        }
        public static AddBookResponse MapDomainToDto(BookDomain book)
        {
            return mapper.Map<AddBookResponse>(book);
        }
    }
}
