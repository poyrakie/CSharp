
namespace SubmissionTask.ClassLibrary.Interfaces;

///<summary>
/// Representerar ett interface för kontaktrepository, 
/// innehåller fyra metoder för att lägga till, ta bort, hämta lista och skanna listan.
///</summary>
public interface IContactRepository
{
    event EventHandler? ContactListUpdated;

    public bool AddToList(IContact contact);
    public IEnumerable<IContact> GetAllFromList();
    public bool RemoveFromList(string email);
    public bool ScanListForEmail(string email);
}