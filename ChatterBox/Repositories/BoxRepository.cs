using ChatterBox.Data;
using ChatterBox.Entities;
using ChatterBox.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ChatterBox.Repositories
{
    public class BoxRepository : DBRepository<Box>, IBoxRepository
    {
        public BoxRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Box>> GetByNameAsync(string name)
        {
            return await _dbSet.Where(b => b.BoxName == name).ToListAsync();
        }
    }
}
