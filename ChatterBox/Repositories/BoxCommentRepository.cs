using ChatterBox.Data;
using ChatterBox.Entities;
using ChatterBox.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatterBox.Repositories
{
    public class BoxCommentRepository : DBRepository<BoxComment>, IBoxCommentRepository
    {
        public BoxCommentRepository(AppDbContext context) :base(context) { }

        public async Task<IEnumerable<BoxComment>> GetByChatBoxIdAsync(int chatBoxId)
        {
            return await _dbSet.Where(boxComment => boxComment.ChatBoxId == chatBoxId).ToListAsync();
        }

        public async Task<IEnumerable<BoxComment>> GetByNameAsync(string name)
        {
            return await _dbSet.Where(boxComment => boxComment.Name == name).ToListAsync();
        }
    }
}
