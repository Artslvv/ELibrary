using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer;
using ELibrary.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.Accounts
{
    public class LoginRequest : IRequest<string>
    {
        public string Login { get; }
        public string Password { get; }

        public LoginRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, string>
    {
        private readonly DataContext _dataContext;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginRequestHandler(DataContext dataContext, IJwtGenerator jwtGenerator)
        {
            _dataContext = dataContext;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstAsync(
                user1 => user1.Login == request.Login && user1.Password == request.Password,
                cancellationToken: cancellationToken);
            var token = _jwtGenerator.Create(user.Login,user.Role.ToString(), user.Age.ToString(),user.Id.ToString());
            return token;
        }
    }
}