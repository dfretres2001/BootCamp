using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class DepositController : BaseApiController
{
    private readonly IDepositService _depositService;

    public DepositController(IDepositService depositService)
    {
        _depositService = depositService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepositModel request)
    {
        return Ok(await _depositService.Add(request));
    }
}
