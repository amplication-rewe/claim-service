using ClaimService.Infrastructure;

namespace ClaimService.APIs;

public class ClaimsService : ClaimsServiceBase
{
    public ClaimsService(ClaimServiceDbContext context)
        : base(context) { }
}
