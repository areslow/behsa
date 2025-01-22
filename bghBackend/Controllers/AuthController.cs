using bghBackend.Application.Auth;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bghBackend.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthManager _authManager;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private string _secretKey;

        public AuthController(
            ApplicationDbContext db,
            //UserManager<AppUser> userManager,
            //RoleManager<IdentityRole> roleManager,
            //string secretKey,
            IAuthManager authManager)
        {
            _db = db;
            //_userManager = userManager;
            //_roleManager = roleManager;

            //_secretKey = secretKey;
            _authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login([FromBody]LoginRequestDTO request)
        {
            return await _authManager.Login(request);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDTO model)
        {
            return await _authManager.CreateUser(model);
        }


    }
}
