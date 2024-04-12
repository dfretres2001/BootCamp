using Core.Interfaces.Services;
using Core.Request;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountService _service;

    public AccountController(IAccountService accountService)
    {
        _service = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountModel request)
    {
        return Ok(await _service.Add(request));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountModel request)
    {
        return Ok(await _service.Update(request));
    }
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterAccountModel filter)
    {
        var account = await _service.GetFiltered(filter);
        return Ok(account);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
}
