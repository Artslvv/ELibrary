using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using ELibrary.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.Book.Queries
{
    public class SearchBook : IRequest<IEnumerable<BookDetailsDto>>
    {
        public string Search { get; }

        public SearchBook(string search)
        {
            Search = search;
        }
        public class SearchBookHandler : IRequestHandler<SearchBook, IEnumerable<BookDetailsDto>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public SearchBookHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<IEnumerable<BookDetailsDto>> Handle(SearchBook request, CancellationToken cancellationToken)
            {
                var book =  _dataContext.Books
                    .AsNoTracking()
                    .Where(book1 => (book1.Name
                        .ToLower().Contains(request.Search.ToLower())==true ||
                                     book1.Price.ToString()
                        .ToLower().Contains(request.Search.ToLower())==true)||
                                    (book1.Available.ToString()
                        .ToLower().Contains(request.Search.ToLower())==true)||
                                    (book1.AgeLimit.ToString()
                        .ToLower().Contains(request.Search.ToLower())==true));
                await Task.Yield();
                return  _mapper.Map<IEnumerable<BookDetailsDto>>(book);
            }
        }
    }
}