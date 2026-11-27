using MVC_Gym.Entities;
using System.Linq.Expressions;

namespace MVC_Gym.Repositories.Interfaces
{
    public interface IMembershipRepositories
    {
        Task<bool> AnyAsync(Expression<Func<Membership, bool>> predicate, CancellationToken ct = default);
    }
}
