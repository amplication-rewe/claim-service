using ClaimService.APIs;

namespace ClaimService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IReviewsService, ReviewsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
