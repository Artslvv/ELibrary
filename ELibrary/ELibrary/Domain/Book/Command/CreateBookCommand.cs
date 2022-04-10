using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using ELibrary.Models;
using MediatR;

namespace ELibrary.Domain.Book.Command
{
    public class CreateBookCommand : IRequest
    {
        public int Id { get; }
        public int Price { get; }
        public int Purchases { get;  }
        public int AgeLimit { get;  }
        public string Name { get;  }
        public bool Available { get; }

        public CreateBookCommand(BookDtoCreate bookDtoCreate)
        {
            Id = bookDtoCreate.Id;
            Price = bookDtoCreate.Price;
            Purchases = 0;
            AgeLimit = bookDtoCreate.AgeLimit;
            Name = bookDtoCreate.Name;
            Available = bookDtoCreate.Available;
        }

        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
        {
            private readonly DataContext _dataContext;

            public CreateBookCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                await _dataContext.Books.AddAsync(new DataAccessLayer.Models.Book()
                {
                    Id = request.Id,
                    Price = request.Price,
                    Purchases = request.Purchases,
                    AgeLimit =request.AgeLimit,
                    Name = request.Name,
                    Available = request.Available
                }, cancellationToken);

                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}