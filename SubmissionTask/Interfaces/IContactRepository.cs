namespace SubmissionTask.Interfaces;

///<summary>
/// Representerar ett interface för kontaktrepository, 
/// innehåller fyra metoder för att lägga till, ta bort, hämta lista och skanna listan.
///</summary>
public interface IContactRepository
{
    public bool AddToList(IContact contact);
    public IEnumerable<IContact> GetAllFromList();
    public bool RemoveFromList(string email, int i);
    public bool ScanListForEmail(string email);
}