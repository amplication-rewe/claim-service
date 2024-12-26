namespace ClaimService.APIs.Dtos;

public class CustomerWhereInput
{
    public List<string>? Claims { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
