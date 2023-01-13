using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dumptruck_v4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Dumptruck_v4.Services;
using Microsoft.AspNetCore.Authorization;

namespace Dumptruck_v4.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private SignInManager<ScoobyUser> _signInManager;
        private UserManager<ScoobyUser> _userManager;
        private TokenService _tokenService;

        private ScoobyContext myContext;
        public LoginController(
            SignInManager<ScoobyUser> signInManager,
            UserManager<ScoobyUser> userManager,
            ScoobyContext context,
            TokenService tokenService
            ) { 
            myContext = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        // Maybe should be using interfaces??
        public async Task<ActionResult> Register(LoginDto newUser) {
            // use microsoft identity maybe or the one that was created.
            // One option is to hardcode a default identity in a different app then use same DB maybe?
            // or should prob be able to use appsettings.secrets.json but that doesn;t help deployment.
            
            //Supposed to validate the LoginDto, ie. I guess that user doesn;t already exist, password is suitable etc, used email if applicable

            var user = new ScoobyUser { UserName = newUser.UserName };
            var result = await _userManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded) {
                await _signInManager.SignInAsync(user, isPersistent: false);
                string jwt = _tokenService.CreateJwt(user);// create jwt here??
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public LoggedInDto Login(LoginDto userLogin) {
            //validate login details?
            // Check user exists

            // Check user password
            //or use signin manager
            var user = myContext.ScoobyUsers.FirstOrDefault(x => x.UserName.Equals(userLogin.UserName));
            var result = _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, lockoutOnFailure: false);

            if (!result.IsCompletedSuccessfully) {
                throw new HttpRequestException("UserName does not match password");
            }
            // Create jwt
            string jwt = _tokenService.CreateJwt(user);
            // return jwt
            var response = new LoggedInDto() {
                UserName = userLogin.UserName,
                AccessToken = jwt
            };
            return response;
        }

        [HttpGet]
        public void Logout() {
            // get user id from auth token
        }
      
    }
    
}
