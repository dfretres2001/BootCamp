﻿

namespace Core.Entities;

public class Deposit
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DepositDateTime { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}