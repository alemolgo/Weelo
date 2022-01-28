using co_weelo_testproject_service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using co_weelo_testproject_common.Response;
using co_weelo_testproject_common.Request;

namespace co_weelo_testproject_sl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Authentificate([FromBody] AuthRequest authRequest)
        {
            GenericResponse genericResponse = new();

            var authResponse = _authService.Authenticate(authRequest);
            if (authResponse == null)
            {
                genericResponse = new GenericResponse
                {
                    Success = false,
                    Message = "Usario y/o contraseña incorrectos",
                };
                return BadRequest(genericResponse);
            }

            genericResponse.Success = true;
            genericResponse.ResultObject = authResponse;
            return Ok(genericResponse);
        }
    }
}
