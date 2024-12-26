using ClaimService.Infrastructure;

namespace ClaimService.APIs;

public class ReviewsService : ReviewsServiceBase
{
    public ReviewsService(ClaimServiceDbContext context)
        : base(context) { }
}
