using DataAccessLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary.Domain.Book.Command
{
    public class BookUserBuyCommand : IRequest
    {
        public int Id { get; }

        public int UserId { get; }

        public BookUserBuyCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
        public class BookUserBuyCommandHandler : IRequestHandler<BookUserBuyCommand>
        {
            private readonly DataContext _dataContext;

            public BookUserBuyCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(BookUserBuyCommand request, CancellationToken cancellationToken)
            {
                var book = await _dataContext.Books
                    .FirstAsync(book1 => book1.Id == request.Id, cancellationToken);
                var user = await _dataContext.Users.Include(user => user.Books)
                 .FirstAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken);
                if (book.AgeLimit <= user.Age && book.Available != false && !user.Books.Contains(book))
                {
                    user.Books.Add(book);
                    book.Purchases += 1;
                    await _dataContext.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}