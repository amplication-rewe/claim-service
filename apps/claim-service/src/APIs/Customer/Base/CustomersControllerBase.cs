using ClaimService.APIs;
using ClaimService.APIs.Common;
using ClaimService.APIs.Dtos;
using ClaimService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Claims records to Customer
    /// </summary>
    [HttpPost("{Id}/claims")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ClaimWhereUniqueInput[] claimsId
    )
    {
        try
        {
            await _service.ConnectClaims(uniqueId, claimsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Claims records from Customer
    /// </summary>
    [HttpDelete("{Id}/claims")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ClaimWhereUniqueInput[] claimsId
    )
    {
        try
        {
            await _service.DisconnectClaims(uniqueId, claimsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Claims records for Customer
    /// </summary>
    [HttpGet("{Id}/claims")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Claim>>> FindClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ClaimFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindClaims(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Claims records for Customer
    /// </summary>
    [HttpPatch("{Id}/claims")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ClaimWhereUniqueInput[] claimsId
    )
    {
        try
        {
            await _service.UpdateClaims(uniqueId, claimsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
