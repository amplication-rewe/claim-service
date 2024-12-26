using ClaimService.Infrastructure;

namespace ClaimService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(ClaimServiceDbContext context)
        : base(context) { }
}
