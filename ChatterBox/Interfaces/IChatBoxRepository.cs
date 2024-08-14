using ChatterBox.Entities;

namespace ChatterBox.Interfaces
{
    public interface IChatBoxRepository : IRepository<ChatBox>
    {
        Task<IEnumerable<ChatBox>> GetByNameAsync(string name);
        Task<IEnumerable<ChatBox>> GetByBoxIdAsync(int boxId);
    }
}
