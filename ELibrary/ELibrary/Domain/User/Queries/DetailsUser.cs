using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using ELibrary.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.User.Queries
{
    public class DetailsUser : IRequest<UserDetailsDto>
    {
        public int Id { get; }

        public DetailsUser(int id)
        {
            Id = id;
        }

        public class DetailsHandler : IRequestHandler<DetailsUser, UserDetailsDto>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public DetailsHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<UserDetailsDto> Handle(DetailsUser request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users
                    .Include(user1 => user1.Books)
                    .AsNoTracking()
                    .FirstAsync(user1 => user1.Id == request.Id, cancellationToken: cancellationToken);
                return _mapper.Map<UserDetailsDto>(user);
            }
        }
    }
}