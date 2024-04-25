using Core.Entities;
using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class TransactionController : BaseApiController
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredTransactions([FromQuery] FilterTransactionModel filters)
    {
        return Ok(await _transactionService.GetFilteredTransactions(filters));
    }
}
