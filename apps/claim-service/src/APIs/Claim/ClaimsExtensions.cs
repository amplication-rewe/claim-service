using ClaimService.APIs.Dtos;
using ClaimService.Infrastructure.Models;

namespace ClaimService.APIs.Extensions;

public static class ClaimsExtensions
{
    public static Claim ToDto(this ClaimDbModel model)
    {
        return new Claim
        {
            ClaimAmount = model.ClaimAmount,
            ClaimDate = model.ClaimDate,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            PolicyNumber = model.PolicyNumber,
            Reviews = model.Reviews?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ClaimDbModel ToModel(
        this ClaimUpdateInput updateDto,
        ClaimWhereUniqueInput uniqueId
    )
    {
        var claim = new ClaimDbModel
        {
            Id = uniqueId.Id,
            ClaimAmount = updateDto.ClaimAmount,
            ClaimDate = updateDto.ClaimDate,
            PolicyNumber = updateDto.PolicyNumber
        };

        if (updateDto.CreatedAt != null)
        {
            claim.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            claim.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            claim.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return claim;
    }
}
