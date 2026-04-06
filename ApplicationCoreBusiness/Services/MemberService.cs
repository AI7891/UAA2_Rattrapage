using ApplicationCoreBusiness.DTOs;
using ApplicationCoreBusiness.Interfaces.IRepositories;
using ApplicationCoreBusiness.Interfaces.IServices;
using DomainEntityModels.Enums;
using DomainEntityModels.Models;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace ApplicationCoreBusiness.Services
{
    public class MemberService : IMemberService
    {
        #region IMemberRepositoryInjection
        // This section defines a private readonly field for the IMember_Repository and a constructor that takes an IMember_Repository as a parameter. The constructor checks if the memberRepository is null and throws an ArgumentNullException if it is, ensuring that the service has a valid repository to work with.
        private readonly IMember_Repository _memberRepository;
        // Constructor for the MemberService class that takes an IMember_Repository as a parameter and assigns it to the private readonly field _memberRepository. This allows the service to interact with the repository to perform operations related to members.
        public MemberService(IMember_Repository memberRepository)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }
        #endregion


        public async Task<Member> CreateMemberAsync(CreateMemberDto member)
        {
            // Check if a member with the same email already exists in the repository. If it does, throw an InvalidOperationException to prevent duplicate entries. This ensures that each member has a unique email address in the system.
            var existingMember = _memberRepository.GetMemberByEmailAsync(member.Email).Result;
            // If a member with the provided email already exists, throw an exception to prevent creating a duplicate member. This check is crucial to maintain data integrity and ensure that each member has a unique email address in the system.
            if (existingMember != null)
            {
                throw new InvalidOperationException($"A member with the email '{member.Email}' already exists.");
            }
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            // Create a new member using the CreateNewAdmin factory method, passing in the necessary details from the CreateMemberDto. The new member is initialized with an active status. This approach encapsulates the creation logic within the Member class, ensuring that all necessary properties are set correctly when creating a new member.
            var newMember = Member.CreateNewAdmin(member.Name, member.Description, member.Email, member.Phone, MemberStatus.Active);
            // Save the newly created member to the repository asynchronously. This allows the service to persist the new member in the data store, making it available for future retrieval and operations.
            await _memberRepository.CreateMemberAsync(newMember);
            return newMember;
        }

        public async Task DeleteMemberAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            await _memberRepository.DeleteByEmailAsync(email);
        }

        public async Task<Func<IEnumerable<Member>>> GetAllMembersAsync()
        {
            var members = _memberRepository.GetAllAsync().Result;
            return members;
        }

        public async Task<Member> GetMemberByEmailAsync(string email)
        {
            // Retrieve an existing member by their email from the repository. This method is currently not implemented, but it is intended to fetch a member's details based on their email address. The result of this operation would typically be used to display member information or perform further operations on the member.
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            var existingMember = _memberRepository.GetMemberByEmailAsync(email).Result;
            // If a member is not found with the provided email, throw an exception to indicate that the member does not exist. This check is important to ensure that the service does not attempt to operate on a non-existent member, which could lead to errors or inconsistent data states.
            if (existingMember == null)
            {
                throw new InvalidOperationException($"A member with the email '{email}' does not exists.");
            }

            return existingMember!;

        }

        public async Task<Member> GetMemberByIDAsync(int id)
        {
            // Retrieve an existing member by their ID from the repository. This method is currently not implemented, but it is intended to fetch a member's details based on their unique identifier. The result of this operation would typically be used to display member information or perform further operations on the member.
            var existingMember = await _memberRepository.GetMemberByIdAsync(id);
            // If a member is found with the provided ID, throw an exception to indicate that the member already exist. This check is important to ensure that the service does not attempt to operate on a non-existent member, which could lead to errors or inconsistent data states.
            if (existingMember != null)
            {
                throw new InvalidOperationException($"A member with the email '{existingMember.Id}' already exists.");
            }
            return existingMember!;

        }

        public async Task<Member> UpdateMemberAsync(UpdateMemberDto member)
        {
            // Check if the provided member object is null. If it is, throw an ArgumentNullException to indicate that a valid member object must be provided for the update operation. This check ensures that the service does not attempt to update a null member, which would lead to errors.
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            // Check if the email property of the member is null or whitespace. If it is, throw an ArgumentNullException to indicate that a valid email address must be provided for the update operation. This check ensures that the service does not attempt to update a member without a valid email, which is crucial for identifying the member in the repository.
            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentNullException(nameof(member.Email));
            // Check if a member with the same email already exists in the repository. If it does, throw an InvalidOperationException to prevent duplicate entries. This ensures that each member has a unique email address in the system, even when updating existing members.
            var existingMember = _memberRepository.GetMemberByEmailAsync(member.Email!).Result;
            // If a member with the provided email already exists, throw an exception to prevent updating to a duplicate email. This check is crucial to maintain data integrity and ensure that each member has a unique email address in the system, even when updating member information.
            if (existingMember == null)
            {
                throw new InvalidOperationException(
                    $"A member with the email '{member.Email}' doesn't exists.");
            }
            // Update the member's details in the repository asynchronously. This allows the service to persist any changes made to the member's information, ensuring that the data store reflects the most current state of the member.

            existingMember.UpdateFullMember(member.Name ?? existingMember.Name, member.Description ?? existingMember.Description, member.Email, member.Phone ?? existingMember.Phone, existingMember.Status);

            await _memberRepository.UpdateMemberAsync(existingMember);
            return existingMember;
        }
    }
}
