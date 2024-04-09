﻿using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Request;
using Core.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace WebApi.Controllers;

public class CurrencyController : BaseApiController
{
    private readonly ICurrencyService _service;

    public CurrencyController(ICurrencyService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCurrencyModel request)
    {
        return Ok(await _service.Add(request));
    }
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterCurrencyModel filter)
    {
        var curriencies = await _service.GetFiltered(filter);
        return Ok(curriencies);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var currency = await _service.GetById(id);
        return Ok(currency);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCurrencyModel request)
    {
        return Ok(await _service.Update(request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
}
