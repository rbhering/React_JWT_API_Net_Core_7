using api.Data;
using api.Models;
using api.Models.Entities;
using api.Services.Passwords;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

            // Auto migrate and add example user
            //_context.Database.Migrate();

            if (!_context.User.Any())
            {
                var user = new User { RefreshToken="fsadfds", Nome = "admin", Email = "admin@gmail.com", Password = _passwordHasher.HashPassword("123456") };

                _context.Add(user);

                _context.SaveChanges();
            } 
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            List<User> users = _context.User.ToList();

            var user = await _context.User.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            return user;

        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

      

        public async Task<User> GetById(int id)
        {
            return await _context.User.FirstAsync(x => x.Id == id);
        }

        public async Task<int> Delete(int id)
        {
            var user = await _context
                    .User
                    .FirstOrDefaultAsync(x => x.Id == id);

            if(user!=null)
                _context.User.Remove(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<User> Create(User user)
        {
            await _context
                   .User
                   .AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<User> Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified; 
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
