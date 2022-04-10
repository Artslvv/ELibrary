using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.User.Command
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly DataContext _dataContext;

            public DeleteUserCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users
                    .FirstAsync(user1 => user1.Id == request.Id, cancellationToken: cancellationToken);
                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}