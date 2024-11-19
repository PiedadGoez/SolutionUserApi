using Application.Common;
using Application.Interfaces;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApiDbContext _dbContext;

        public UserRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Pagination<User>> GetByFilterAsync(string? firstName, string? lastName, int page, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<User> query = _dbContext.Users;

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(u => u.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(u => u.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));

            var totalRecords = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Pagination<User>(items, totalRecords, page, pageSize);
        }

        public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
        {

            var User = await _dbContext.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(new object[] { userId }, cancellationToken);

            if (user == null)
            {               
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken); 

            return true; 
        }
    }
}
