using ApplicationCoreBusiness.Interfaces.IRepositories;
using DomainEntityModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureDatabase.Repositories
{
    public class MemberRepository : IMember_Repository
    {
        #region DBContext Injection
        // This section defines a private readonly field for the AppDBContext and a constructor that takes an AppDBContext as a parameter. The constructor checks if the dbContext is null and throws an ArgumentNullException if it is, ensuring that the repository has a valid database context to work with.
        private readonly AppDBContext _dbContext;
        // Constructor for the MemberRepository class that takes an AppDBContext as a parameter and assigns it to the private readonly field _dbContext. This allows the repository to interact with the database using Entity Framework Core.
        public MemberRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        } 
        #endregion

        public async Task<Member> CreateMemberAsync(Member member)
        {
            // Use AddAsync to add the new member to the database, and then call SaveChangesAsync to persist the changes
            await _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return member;
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.Email == email);

            if (member == null)
                throw new InvalidOperationException($"Member with email '{email}' not found.");

            _dbContext.Members.Remove(member);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<Func<IEnumerable<Member>>> GetAllAsync()
        {
            // Use ToListAsync to retrieve all members from the database, and return an empty list if there are no members
            var members = await _dbContext.Members.ToListAsync()?? Enumerable.Empty<Member>();
            return () => members;
        }

        public async Task<Member?> GetMemberByEmailAsync(string email)
        {
            // Use FirstOrDefaultAsync to find the member by email, which will return null if not found
            var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.Email == email);
            return member;
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            // Use FirstOrDefaultAsync to find the member by ID, which will return null if not found
            var member =( await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == id))!;
            return member;
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            // Use Update to update the member in the database, and then call SaveChangesAsync to persist the changes
            _dbContext.Members.Update(member);
            // Save the changes to the database
            await _dbContext.SaveChangesAsync();
            return member;
        }
    }
}
