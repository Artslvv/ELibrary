using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;
using ELibrary.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELibrary.Domain.User.Queries
{
    public class ListUsersQuery: IRequest<IEnumerable<UserDto>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public ListUsersQuery(int skip,int take)
        {
            Skip = skip;
            Take = take;
        }
    }
    public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
    {
        public ListUsersQueryValidator()
        {
            RuleFor(query => query.Skip).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
            RuleFor(query => query.Take).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }

    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IEnumerable<UserDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ListUsersQueryHandler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var usersData = await _dataContext.Users
                .Skip(request.Skip)
                .Take(request.Take)
                .Include(user => user.Books)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserDto>>(usersData);
        }
    }
}