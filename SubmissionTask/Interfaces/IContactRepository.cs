namespace SubmissionTask.Interfaces;

public interface IContactRepository
{
    public bool AddToList(IContact contact);
    public IEnumerable<IContact> GetAllFromList();
    public bool RemoveFromList(string email, int i);
    public bool ScanListForEmail(string email);
}