using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using ELibrary.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.Book.Queries
{
    public class ListBooksQuery: IRequest<IEnumerable<BookDto>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public ListBooksQuery(int skip,int take)
        {
            Skip = skip;
            Take = take;
        }
    }
    public class ListBooksQueryValidator : AbstractValidator<ListBooksQuery>
    {
        public ListBooksQueryValidator()
        {
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
            RuleFor(query => query.Take).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }

    public class ListBooksQueryHandler : IRequestHandler<ListBooksQuery, IEnumerable<BookDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ListBooksQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            var booksData = await _dataContext.Books.AsNoTracking()
                .Skip(request.Skip)
                .Take(request.Take)
                .Include(user => user.Users)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<BookDto>>(booksData);
        }
    }
}