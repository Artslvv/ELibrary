using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.Book.Command
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; }

        public DeleteBookCommand(int id)
        {
            Id = id;
        }

        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
        {
            private readonly DataContext _dataContext;

            public DeleteBookCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                var book = await _dataContext.Books
                    .FirstAsync(book1 => book1.Id == request.Id, cancellationToken: cancellationToken);
                _dataContext.Books.Remove(book);
                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}