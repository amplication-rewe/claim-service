namespace ClaimService.APIs.Dtos;

public class ClaimUpdateInput
{
    public double? ClaimAmount { get; set; }

    public DateTime? ClaimDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public string? PolicyNumber { get; set; }

    public List<string>? Reviews { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
