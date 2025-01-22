using bghBackend.Application.Auth;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace bghBackend.Infra.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKEy;

        public AuthManager(ApplicationDbContext db,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<AppUser> userManager,
                           IConfiguration configuration)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            secretKEy = configuration.GetValue<string>("ApiSettings:Secret")!;
        }

        /// <summary>
        /// create a user by specified info in user parameter with 
        /// password of the given password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateUser(RegisterRequestDTO regRequest)
        {
            try
            {
                AppUser? userFromDb = await _db.Users.FirstOrDefaultAsync(u => u.UserName!.ToLower() == regRequest.UserName!.ToLower());
                if (userFromDb != null && userFromDb.IsDeleted == false)
                    return ApiResponse.FailureResponse(SD.USERNAME_EXISTS);
                AppUser userToCreate = new()
                {
                    LastName = regRequest.LastName,
                    FirstName = regRequest.FirstName,
                    Email = regRequest.Email,
                    UserName = regRequest.UserName,
                    PhoneNumber = regRequest.PhoneNumber
                };
                var result = await _userManager.CreateAsync(userToCreate, regRequest.Password);
                if (!result.Succeeded) return ApiResponse.FailureResponse(SD.CREATION_FAILED + " if the peroblem persists please contact to admin");
                //if (!string.IsNullOrEmpty (regRequest.Role)) making rol of all registered users to "member". only admin can asign a role or change roles of a user
                await _userManager.AddToRoleAsync(userToCreate, SD.ROLE_MEMBER);
                return ApiResponse.SuccessResponse(SD.USER_CREATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }


        public async Task<ApiResponse> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                AppUser? userFromDb = await _db.Users.FirstOrDefaultAsync(u => u.UserName!.ToLower() == loginRequest.UserName.ToLower());
                if (userFromDb == null || userFromDb.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.WRONG_LOGIN_INFO);
                bool isValid = await _userManager.CheckPasswordAsync(userFromDb, loginRequest.Password);
                if (!isValid) return ApiResponse.FailureResponse(SD.WRONG_LOGIN_INFO);

                
                // get roles of the user
                var roles = await _userManager.GetRolesAsync(userFromDb);

                #region JWT Token
                byte[] key = Encoding.ASCII.GetBytes(secretKEy);// used as parameter of SymmetricSecurityKey

                var List = new List<Claim>()
                {
                   
                };

                var claims = new List<Claim>()
                {
                    new Claim("userName", userFromDb.UserName ?? ""),
                    new Claim("fullName", userFromDb.FirstName + " " + userFromDb.LastName),
                    new Claim("id", userFromDb.Id.ToString()),
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                // token descriptor
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(claims),

                    Expires = DateTime.UtcNow.AddDays(SD.JWT_TOKEN_EXPIRE_DAYS),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                };
                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                #endregion

                // login
                LoginResponseDTO loginResponse = new()
                {
                    UserName = userFromDb.UserName,// we should send more data when logging in so that it can be shown to the user when get signed in
                    Token = tokenHandler.WriteToken(token)
                };
                if (loginResponse.UserName == null || string.IsNullOrEmpty(loginResponse.Token))
                    return ApiResponse.FailureResponse(SD.WRONG_LOGIN_INFO);
                return ApiResponse.SuccessResponse(SD.SIGNIN_SUCCESSFULL, loginResponse);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }


        // were using jwt to do the login so for logout we only need to 
        // remove the stored token from the user localStorage.
        // unless we want to block someone or ...
        // later maybe implemented
        public Task<ApiResponse> Logout(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
