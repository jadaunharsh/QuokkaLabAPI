using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuokkaLabAPI.Data;
using QuokkaLabAPI.Models;
using RestAPIwithJWT.Models.DTOs.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuokkaLabAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Route("profile/userId={id}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var profile = await _context.Users
                             .FirstOrDefaultAsync(up => up.Id == userId);

            if (profile == null)
            {
                return NotFound("Profile not found.");
            }

            return Ok(profile);
        }
    }
}
