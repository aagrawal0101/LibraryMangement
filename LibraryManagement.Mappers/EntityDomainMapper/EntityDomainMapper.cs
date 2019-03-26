
using AutoMapper;
using LibraryModel.Domain;
using LibraryModel.Dto.Contracts;
using LibraryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Mappers.EntityDomainMapper
{
    public class EntityDomainMapper
    {
        private static IMapper mapper = ConfigMapper();

        public static IMapper ConfigMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookEntity, BookDomain>()
                .ForMember(dest => dest.BookId, m => m.MapFrom(src => src.BookId))
                .ForMember(dest => dest.BookName, m => m.MapFrom(src => src.BookName))
                .ForMember(dest => dest.BookType, m => m.MapFrom(src => src.BookType))
                .ForMember(dest => dest.BookAuthor, m => m.MapFrom(src => src.BookAuthor))
                .ReverseMap();

            });
            return mapper = config.CreateMapper();
        }
        public static BookEntity MapEntityToDomain(BookDomain book)
        {
            return mapper.Map<BookEntity>(book);
        }

        public static BookDomain MapEntityToDomains(BookEntity entity)
        {
            return mapper.Map<BookDomain>(entity);
        }

        public static IList<BookDomain> MapEntityListToDomain(IList<BookEntity> GetLists)
        {
            return mapper.Map<IList<BookDomain>>(GetLists);

        }
    }
}