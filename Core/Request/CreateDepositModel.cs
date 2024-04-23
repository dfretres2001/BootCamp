﻿

namespace Core.Request;

public class CreateDepositModel
{
    public decimal Amount { get; set; }

    public DateTime DepositDateTime { get; set; }

    public int AccountId { get; set; }

    public int BankId { get; set; }

}
