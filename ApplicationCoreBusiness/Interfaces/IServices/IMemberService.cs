using ApplicationCoreBusiness.DTOs;
using DomainEntityModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCoreBusiness.Interfaces.IServices
{
    public interface IMemberService
    {
        public Task<Member> CreateMemberAsync(CreateMemberDto member);
        public Task<Member> UpdateMemberAsync(UpdateMemberDto member);
        public Task DeleteMemberAsync(string email);
        public Task<Member> GetMemberByIDAsync(int id);
        public Task<Member> GetMemberByEmailAsync(string email);
        public Task<Func<IEnumerable<Member>>> GetAllMembersAsync();
    }
}
