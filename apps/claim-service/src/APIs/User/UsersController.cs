using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
