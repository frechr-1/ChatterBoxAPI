using ChatterBox.Entities;

namespace ChatterBox.Interfaces
{
    public interface IBoxRepository : IRepository<Box>
    {
        Task<IEnumerable<Box>> GetByNameAsync(string name);
    }
}
