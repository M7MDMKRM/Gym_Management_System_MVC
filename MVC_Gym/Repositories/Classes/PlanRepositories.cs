using Microsoft.EntityFrameworkCore;
using MVC_Gym.Contexts;
using MVC_Gym.Entities;
using MVC_Gym.Repositories.Entites;

namespace MVC_Gym.Repositories.Classes
{
    public class PlanRepositories : IPlanRepositorie
    {

        private readonly GymDbContext _dbContext ;
        public PlanRepositories(GymDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Plans.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbContext.Plans.FindAsync( [id] , ct);
        }

        public async Task<int> UpdateAsync(CancellationToken ct = default)
        {
            try 
            {
                return await _dbContext.SaveChangesAsync(ct);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
                return 0;
            }
        }
    }
}
