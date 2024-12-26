using ClaimService.APIs;
using ClaimService.APIs.Common;
using ClaimService.APIs.Dtos;
using ClaimService.APIs.Errors;
using ClaimService.APIs.Extensions;
using ClaimService.Infrastructure;
using ClaimService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimService.APIs;

public abstract class ClaimsServiceBase : IClaimsService
{
    protected readonly ClaimServiceDbContext _context;

    public ClaimsServiceBase(ClaimServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Claim
    /// </summary>
    public async Task<Claim> CreateClaim(ClaimCreateInput createDto)
    {
        var claim = new ClaimDbModel
        {
            ClaimAmount = createDto.ClaimAmount,
            ClaimDate = createDto.ClaimDate,
            CreatedAt = createDto.CreatedAt,
            PolicyNumber = createDto.PolicyNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            claim.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            claim.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Reviews != null)
        {
            claim.Reviews = await _context
                .Reviews.Where(review => createDto.Reviews.Select(t => t.Id).Contains(review.Id))
                .ToListAsync();
        }

        _context.Claims.Add(claim);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ClaimDbModel>(claim.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Claim
    /// </summary>
    public async Task DeleteClaim(ClaimWhereUniqueInput uniqueId)
    {
        var claim = await _context.Claims.FindAsync(uniqueId.Id);
        if (claim == null)
        {
            throw new NotFoundException();
        }

        _context.Claims.Remove(claim);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Claims
    /// </summary>
    public async Task<List<Claim>> Claims(ClaimFindManyArgs findManyArgs)
    {
        var claims = await _context
            .Claims.Include(x => x.Customer)
            .Include(x => x.Reviews)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return claims.ConvertAll(claim => claim.ToDto());
    }

    /// <summary>
    /// Meta data about Claim records
    /// </summary>
    public async Task<MetadataDto> ClaimsMeta(ClaimFindManyArgs findManyArgs)
    {
        var count = await _context.Claims.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Claim
    /// </summary>
    public async Task<Claim> Claim(ClaimWhereUniqueInput uniqueId)
    {
        var claims = await this.Claims(
            new ClaimFindManyArgs { Where = new ClaimWhereInput { Id = uniqueId.Id } }
        );
        var claim = claims.FirstOrDefault();
        if (claim == null)
        {
            throw new NotFoundException();
        }

        return claim;
    }

    /// <summary>
    /// Update one Claim
    /// </summary>
    public async Task UpdateClaim(ClaimWhereUniqueInput uniqueId, ClaimUpdateInput updateDto)
    {
        var claim = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            claim.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Reviews != null)
        {
            claim.Reviews = await _context
                .Reviews.Where(review => updateDto.Reviews.Select(t => t).Contains(review.Id))
                .ToListAsync();
        }

        _context.Entry(claim).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Claims.Any(e => e.Id == claim.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Customer record for Claim
    /// </summary>
    public async Task<Customer> GetCustomer(ClaimWhereUniqueInput uniqueId)
    {
        var claim = await _context
            .Claims.Where(claim => claim.Id == uniqueId.Id)
            .Include(claim => claim.Customer)
            .FirstOrDefaultAsync();
        if (claim == null)
        {
            throw new NotFoundException();
        }
        return claim.Customer.ToDto();
    }

    /// <summary>
    /// Connect multiple Reviews records to Claim
    /// </summary>
    public async Task ConnectReviews(
        ClaimWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Claims.Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Reviews);

        foreach (var child in childrenToConnect)
        {
            parent.Reviews.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Reviews records from Claim
    /// </summary>
    public async Task DisconnectReviews(
        ClaimWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Claims.Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Reviews?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Reviews records for Claim
    /// </summary>
    public async Task<List<Review>> FindReviews(
        ClaimWhereUniqueInput uniqueId,
        ReviewFindManyArgs claimFindManyArgs
    )
    {
        var reviews = await _context
            .Reviews.Where(m => m.ClaimId == uniqueId.Id)
            .ApplyWhere(claimFindManyArgs.Where)
            .ApplySkip(claimFindManyArgs.Skip)
            .ApplyTake(claimFindManyArgs.Take)
            .ApplyOrderBy(claimFindManyArgs.SortBy)
            .ToListAsync();

        return reviews.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reviews records for Claim
    /// </summary>
    public async Task UpdateReviews(
        ClaimWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var claim = await _context
            .Claims.Include(t => t.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (claim == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        claim.Reviews = children;
        await _context.SaveChangesAsync();
    }
}
