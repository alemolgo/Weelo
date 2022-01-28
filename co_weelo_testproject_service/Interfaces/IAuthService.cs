using co_weelo_testproject_common.Request;
using co_weelo_testproject_common.Response;

namespace co_weelo_testproject_service.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Authenticate (AuthRequest authRequest);
    }
}
