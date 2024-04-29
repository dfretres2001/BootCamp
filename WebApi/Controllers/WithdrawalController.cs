using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class WithdrawalController : BaseApiController
{
    private readonly IWithdrawalService _withdrawalService;
    public WithdrawalController(IWithdrawalService withdrawalService)
    {
        _withdrawalService = withdrawalService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWithdrawalModel request)
    {
        return Ok(await _withdrawalService.Add(request));
    }
}
