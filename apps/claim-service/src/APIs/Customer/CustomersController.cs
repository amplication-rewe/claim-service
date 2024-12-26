using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
