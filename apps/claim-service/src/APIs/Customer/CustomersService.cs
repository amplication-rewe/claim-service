using ClaimService.Infrastructure;

namespace ClaimService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(ClaimServiceDbContext context)
        : base(context) { }
}
