using SubmissionTask.Interfaces;

namespace SubmissionTask.Models;

public class Contact : IContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Road { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
