using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs;

[ApiController()]
public class ClaimsController : ClaimsControllerBase
{
    public ClaimsController(IClaimsService service)
        : base(service) { }
}
