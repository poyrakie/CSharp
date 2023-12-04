namespace SubmissionTask.Interfaces;

public interface IContact
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    IAddress Address { get; set; }
}
