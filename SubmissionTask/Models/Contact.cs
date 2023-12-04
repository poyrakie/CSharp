using SubmissionTask.Interfaces;

namespace SubmissionTask.Models;

internal class Contact : IContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public IAddress Address { get; set; } = null!;
}
