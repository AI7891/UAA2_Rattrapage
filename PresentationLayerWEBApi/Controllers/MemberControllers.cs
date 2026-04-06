using ApplicationCoreBusiness.DTOs;
using ApplicationCoreBusiness.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayerWEBApi.DTOs_External;

namespace PresentationLayerWEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberControllers : ControllerBase
    {
        // This section defines a private readonly field for the IMemberService and a constructor that takes an IMemberService as a parameter. The constructor checks if the memberService is null and throws an ArgumentNullException if it is, ensuring that the controller has a valid service to work with.
        private readonly IMemberService _memberService;
        // Constructor for the MemberControllers class that takes an IMemberService as a parameter and assigns it to the private readonly field _memberService. This allows the controller to interact with the service to perform operations related to members, such as creating, deleting, and retrieving members.
        public MemberControllers(IMemberService memberService)
        {
            _memberService = memberService??throw new ArgumentNullException(nameof(memberService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterMemberDTo dto)
        {
            if (dto == null)
                return BadRequest("Invalid payload");

            try
            {
                var member = new CreateMemberDto
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    PasswordHash = dto.Password,
                    ConfirmPassword = dto.ConfirmPassword
                };

                var validMember = await _memberService.CreateMemberAsync(member);
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMember([FromBody] UpdateMemberRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dto = new UpdateMemberDto
                {
                    Name = request.Name,
                    Description = request.Description,
                    Email = request.Email,
                    Phone = request.Phone,
                };

                var updatedMember = await _memberService.UpdateMemberAsync(dto);

                return Ok(updatedMember);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while updating the member.",
                    details = ex.Message
                });
            }
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                // Step 1: Retrieve the member by ID
                var member = await _memberService.GetMemberByIDAsync(id);

                // Step 2: Delete the member
                await _memberService.DeleteMemberAsync(member);

                return Ok(new { message = $"Member with ID {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while deleting the member.",
                    details = ex.Message
                });
            }
        }
    }
}
