using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using ELibrary.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.User.Command
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; }

        public string Firstname { get; }
        
        public string Lastname { get; }
        
        public string Login { get; }
        
        public string Password { get; }
        

        public UpdateUserCommand(UserDtoUpdate userDtoUpdate)

        {
            Id = userDtoUpdate.Id;
            Firstname = userDtoUpdate.Firstname;
            Lastname = userDtoUpdate.Lastname;
            Login = userDtoUpdate.Login;
            Password = userDtoUpdate.Password;
        }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly DataContext _dataContext;

            public UpdateUserCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users
                    .FirstAsync(user1 => user1.Id == request.Id, cancellationToken: cancellationToken);
                user.Id = request.Id;
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.Login = request.Login;
                user.Password = request.Password;
                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;

            }
        }
    }
    
}