

namespace SubmissionTask.Interfaces;

public interface IContact
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    string City { get; set; }
    string Road { get; set; }
    string HouseNumber { get; set; }
    string PostalCode { get; set; }
}
