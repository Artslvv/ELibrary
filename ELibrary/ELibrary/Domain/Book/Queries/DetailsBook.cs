using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using ELibrary.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.Book.Queries
{
    public class DetailsBook : IRequest<BookDetailsDto>
    {
        public int Id { get; }

        public DetailsBook(int id)
        {
            Id = id;
        }

        public class DetailsHandler : IRequestHandler<DetailsBook, BookDetailsDto>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public DetailsHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<BookDetailsDto> Handle(DetailsBook request, CancellationToken cancellationToken)
            {
                var book = await _dataContext.Books.AsNoTracking()
                    .Include(book1 => book1.Users)
                    .FirstAsync(book1 => book1.Id == request.Id, cancellationToken: cancellationToken);
                return _mapper.Map<BookDetailsDto>(book);
            }
        }
    }
}