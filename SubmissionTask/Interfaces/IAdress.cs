﻿namespace SubmissionTask.Interfaces;

internal interface IAddress
{
    string City { get; set; }
    string Road { get; set; }
    string HouseNumber { get; set; }
    int PostalCode { get; set; }
}
