namespace SubmissionTask.Interfaces;

public interface IContactService
{
    bool AddToList();
    bool ShowAllContacts();
    bool ShowContact(int i);
    bool DeleteContact(int i);
}
