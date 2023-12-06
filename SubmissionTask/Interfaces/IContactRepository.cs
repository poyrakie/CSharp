namespace SubmissionTask.Interfaces;

public interface IContactRepository
{
    public bool AddToList(IContact contact);
    public IEnumerable<IContact> GetAllFromList();
}