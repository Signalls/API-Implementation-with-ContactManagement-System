using ContactData.Entities;
using ContactModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactData.Repository
{
    public class UserRepository : IUserRepository
    {



        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;







        public UserRepository(IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        public AppUser AddNewUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(LoginDto loginRequestDTO)
        {
            var findEmail = await _userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (findEmail == null)
            {
                throw new Exception("Email does not exist");
            };



            var isValid = await _userManager.CheckPasswordAsync(findEmail, loginRequestDTO.Password);
            if (isValid == false)
            {
                throw new Exception("Wrong Password");
            }



            var result = await _signInManager.PasswordSignInAsync(loginRequestDTO.UserName, loginRequestDTO.Password, isPersistent: false, lockoutOnFailure: false);



            var authClaims = new List<Claim>()
             {
             new Claim(ClaimTypes.Name,loginRequestDTO.UserName),
             new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
             };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );



            if (result.Succeeded)
            {
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new Exception("Account not Found");
            }
            //if  user  was found generate JWT Token




        }
        public async Task<IdentityResult> RegisterAsync(RegisterDto registrationRequestDTO, string role)
        {
            var findEmail = await _userManager.FindByEmailAsync(registrationRequestDTO.UserName);
            if (findEmail != null)
            {
                throw new Exception("UserName already exist");
            }
            if (await _roleManager.RoleExistsAsync(role))
            {
                AppUser user = new AppUser()
                {
                    UserName = registrationRequestDTO.UserName,
                    Email = registrationRequestDTO.UserName,
                    FirstName = registrationRequestDTO.FirstName,
                    LastName = registrationRequestDTO.LastName,
                    Roles = role
                };
                var regUser = await _userManager.CreateAsync(user, registrationRequestDTO.password);
                if (regUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    return regUser;
                }
                else
                {
                    throw new Exception("Registration not completed");
                }
            }
            else
            {
                throw new Exception("Role not exist");
            }
        }












    }
}
