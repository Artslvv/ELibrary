using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.Book.Command
{
    public class UpdateBookCommand : IRequest
    {
        public int Id { get; }
        
        public int Price { get; }
        
        public bool Available { get; }
        

        public UpdateBookCommand(int id, int price,  bool available)

        {
            Id = id;
            Price = price;
            Available = available;
        }
        public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
        {
            private readonly DataContext _dataContext;

            public UpdateBookCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
            {
                var book = await _dataContext.Books
                    .FirstAsync(book1 => book1.Id == request.Id, cancellationToken: cancellationToken);
                book.Price = request.Price;
                book.Available = request.Available;
                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;

            }
        }
    }
    
}