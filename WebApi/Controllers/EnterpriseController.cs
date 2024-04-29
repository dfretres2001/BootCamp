﻿using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class EnterpriseController : BaseApiController
{
    private readonly IEnterpriseService _service;
    public EnterpriseController(IEnterpriseService service)
    {
        _service = service;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
=> Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnterpriseModel request)
    {
        return Ok(await _service.Add(request));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEnterpriseModel request)
    {
        return Ok(await _service.Update(request));
    }
    //[HttpGet("filtered")]
    ////public async Task<IActionResult> GetFiltered([FromQuery] FilterEnterpriseModel filter)
    ////{
    ////    var account = await _service.GetFiltered(filter);
    ////    return Ok(account);
    ////}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
}
