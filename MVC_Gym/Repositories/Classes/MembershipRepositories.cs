using Microsoft.EntityFrameworkCore;
using MVC_Gym.Contexts;
using MVC_Gym.Entities;
using MVC_Gym.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MVC_Gym.Repositories.Classes
{
    public class MembershipRepository : IMembershipRepositories
    {
        private readonly GymDbContext _dbContext;

        public MembershipRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyAsync(Expression<Func<Membership, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbContext.Memberships.AnyAsync(predicate, ct);
        }
    }
}