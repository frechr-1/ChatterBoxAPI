using ChatterBox.Data;
using ChatterBox.Entities;
using ChatterBox.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatterBox.Repositories
{
    public class ChatBoxRepository : DBRepository<ChatBox>, IChatBoxRepository
    {
        public ChatBoxRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ChatBox>> GetByNameAsync(string name)
        {
            return await _dbSet.Where(c => c.Name == name).ToListAsync();
        }
        
        public async Task<IEnumerable<ChatBox>> GetByBoxIdAsync(int boxId)
        {
            return await _dbSet.Where(c => c.BoxId == boxId).ToListAsync();
        }
    }
}
