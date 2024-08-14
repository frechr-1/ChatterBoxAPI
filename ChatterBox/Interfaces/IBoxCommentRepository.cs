using ChatterBox.Entities;

namespace ChatterBox.Interfaces
{
    public interface IBoxCommentRepository : IRepository<BoxComment>
    {
        Task<IEnumerable<BoxComment>> GetByChatBoxIdAsync(int chatBoxId);
    }
}
