namespace SubmissionTask.ClassLibrary.Interfaces;

///<summary>
/// Representerar ett interface för kontaktservice, 
/// Innehåller fyra metoder för att följa crud (minus update).
///</summary>
public interface IContactService
{
    bool AddToList();
    bool ShowAllContacts();
    bool ShowContact(int i);
    bool DeleteContact();
}
