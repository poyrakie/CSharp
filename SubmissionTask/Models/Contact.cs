using SubmissionTask.Interfaces;

namespace SubmissionTask.Models;

public class Contact : IContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public required IAddress Address { get; set; }
}
