using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(AppUser user)
        {
            _context.Entry(user).State = EntityState.Added;
        }

        public void Delete(AppUser user)
        {
            _context.Entry(user).State = EntityState.Deleted;
        }

        public async Task<AppUser> GetUserByName(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(storedUser => storedUser.FirstName.ToLower() == name.ToLower());
        }

        public async Task<AppUser> GetUserByÍd(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}