﻿

using Core.Entities;

namespace Core.Models;

public class DepositDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DepositDateTime { get; set; }
    public string Account { get; set; } = string.Empty; //ojo
}
