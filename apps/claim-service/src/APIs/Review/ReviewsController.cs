using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs;

[ApiController()]
public class ReviewsController : ReviewsControllerBase
{
    public ReviewsController(IReviewsService service)
        : base(service) { }
}
