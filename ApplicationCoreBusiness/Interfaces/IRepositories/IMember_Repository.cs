using DomainEntityModels.Models;

namespace ApplicationCoreBusiness.Interfaces.IRepositories
{
    public interface IMember_Repository
    {
        public Task<Member> CreateMemberAsync(Member member);
        public Task<bool> DeleteMemberAsync(Member member);
        public Task<Member> UpdateMemberAsync(Member member);
        public Task<Member?> GetMemberByIdAsync(int id);
        public Task<Member?> GetMemberByEmailAsync(string email);
        public Task <Func<IEnumerable<Member>>> GetAllAsync ();
    }
}
