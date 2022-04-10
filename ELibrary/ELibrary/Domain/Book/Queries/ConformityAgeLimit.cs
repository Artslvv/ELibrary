using AutoMapper;
using DataAccessLayer;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Domain.Book.Queries
{

    public class ConformityAgeLimit: IRequest<bool>
    {
        public int UserId { get; }
        public int BookId { get; }


        public ConformityAgeLimit(int userid, int bookId)
        {
            UserId = userid;
            BookId = bookId;
        }
        public class ConformityAgeLimitHandler : IRequestHandler<ConformityAgeLimit, bool>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public ConformityAgeLimitHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<bool> Handle(ConformityAgeLimit request, CancellationToken cancellationToken)
            {
                var user = _dataContext.Users.FirstOrDefault(user => user.Id == request.UserId);
                var book = _dataContext.Books.FirstOrDefault(book => book.Id == request.BookId);
                if (book.Available != false && user.Age>= book.AgeLimit)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
