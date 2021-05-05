using API.Entities;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
  public interface IUserRepository
    {
        Task<AppUser> GetUserByName(string name);
        Task<AppUser> GetUserByÍd(int id);
        Task<IEnumerable<AppUser>> GetUsers();
        Task<bool> SaveAllAsync();
        void Add(AppUser user);
        void Update(AppUser user);
        void Delete(AppUser user);

    } 
}
