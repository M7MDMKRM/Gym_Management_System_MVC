using MVC_Gym.Entities;

namespace MVC_Gym.Repositories.Entites;
    public interface IPlanRepositorie
    {
        Task<IEnumerable<Plan>> GetAllAsync(CancellationToken ct = default);
        Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> UpdateAsync( CancellationToken ct = default);





}

