using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using ELibrary.Models;
using MediatR;

namespace ELibrary.Domain.Book.Command
{
    public class CreateUserCommand : IRequest
    {
        public int Id { get; }
        
        public int Role { get; }
        
        public int Age { get;}

        public string Login { get;}
        
        public string Password { get; }
        
        public string Firstname { get; }
        
        public string Lastname { get; }

        public CreateUserCommand(UserDtoCreate userDtoCreate)

        {
            Id = userDtoCreate.Id;
            Role = 1;
            Age = userDtoCreate.Age;
            Login = userDtoCreate.Login;
            Password = userDtoCreate.Password;
            Firstname = userDtoCreate.Firstname;
            Lastname = userDtoCreate.Lastname;

        }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
        {
            private readonly DataContext _dataContext;

            public CreateUserCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                await _dataContext.Users.AddAsync(new DataAccessLayer.Models.User()
                {
                    Id = request.Id,
                    Role = request.Role,
                    Age = request.Age,
                    Login = request.Login,
                    Password = request.Password,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                }, cancellationToken);

                await _dataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}