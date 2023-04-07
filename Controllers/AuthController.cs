using ContactData.Repository;
using ContactModel;
using Microsoft.AspNetCore.Mvc;

namespace ContactAPI.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        //protected ApiResponse _response;





        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            //this._response = new();

        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {

            try
            {
                var result = await _userRepo.Login(model);
                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }



        }




        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model, string role)
        {

            try
            {
                var result = await _userRepo.RegisterAsync(model, role);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



    }
}