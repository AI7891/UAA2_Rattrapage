using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCoreBusiness.DTOs
{
    public class CreateMemberDto
    {
        public  string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
