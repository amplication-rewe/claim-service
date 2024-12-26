namespace ClaimService.APIs.Dtos;

public class ClaimCreateInput
{
    public double? ClaimAmount { get; set; }

    public DateTime? ClaimDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public string? PolicyNumber { get; set; }

    public List<Review>? Reviews { get; set; }

    public DateTime UpdatedAt { get; set; }
}
