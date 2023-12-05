﻿using SubmissionTask.Interfaces;

namespace SubmissionTask.Models;

public class Address : IAddress
{
    public string City { get; set; } = null!;
    public string Road { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public int PostalCode { get; set; }
}