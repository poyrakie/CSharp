namespace SubmissionTask.ClassLibrary.Interfaces;

///<summary>
/// Representerar ett kontaktinterface.
/// Innehåller egenskaper för olika attribut relaterade till en kontakt.
///</summary>
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
