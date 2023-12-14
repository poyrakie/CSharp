using SubmissionTask.Interfaces;

namespace SubmissionTask.Models;

///<summary>
/// Representerar en kontaktmodel och implementerar interface IContact.
/// Innehåller egenskaper för olika attribut relaterade till en kontakt.
///</summary>
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
