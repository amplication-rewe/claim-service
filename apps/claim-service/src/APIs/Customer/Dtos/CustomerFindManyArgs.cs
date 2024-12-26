using ClaimService.APIs.Common;
using ClaimService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }