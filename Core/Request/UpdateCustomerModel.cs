﻿using Core.Constants;

namespace Core.Request;

public class UpdateCustomerModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Lastname { get; set; }

    public string DocumentNumber { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? Mail { get; set; }

    public string? Phone { get; set; }

    public CustomerStatus CustomerStatus { get; set; } = CustomerStatus.Active;

    public int BankId { get; set; }
    public DateTime? Birth { get; set; }
}
