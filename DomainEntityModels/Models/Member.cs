using DomainEntityModels.Enums;
using System.Net.NetworkInformation;

namespace DomainEntityModels.Models
{
    public class Member
    {
        // This class represents a member in the system with properties such as Id, Name, Description, Email, Phone, PasswordHash, and Status.
        #region Properties
        public int Id { get; private set; } = 0;
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty; 
        public MemberStatus Status { get; private set; } = MemberStatus.Unknown;
        #endregion

        #region Constructors
        public Member() { }

        public Member(int id, string name, string description, string email, string phone, MemberStatus memberStatus)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException(nameof(phone));
            }
            Name = name;
            Description = description;
            Email = email;
            Phone = phone;
            Status = memberStatus;
        }

        public Member(string name, string description, string email, string phone, MemberStatus memberStatus) : this(0, name, description, email, phone, memberStatus)
        { }
        #endregion

        #region Accessors
        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }
            PasswordHash = passwordHash;
        }
        public static Member CreateNewAdmin(string name, string description, string email, string phone, MemberStatus memberStatus)
        {
            return new Member(name, description, email, phone, memberStatus);
        }
        public void UpdateFullMember(string name, string description, string email, string phone, MemberStatus memberStatus)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException(nameof(phone));
            }
            Name = name ?? Name;
            Description = description ?? Description ;
            Email = email ?? Email;
            Phone = phone ?? Phone;
            Status = memberStatus;
        }
        public void UpdateMemberStatus(MemberStatus memberStatus)
        {
            Status = memberStatus;
        }   
        public void UpdateMemberPasswordHash(string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }
            PasswordHash = passwordHash;
        }

        #endregion


    }
}
