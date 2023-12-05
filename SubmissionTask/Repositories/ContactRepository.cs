using SubmissionTask.Interfaces;
using SubmissionTask.Models;

namespace SubmissionTask.Repositories;

public class ContactRepository 
{
    private List<IContact> _contactList = [];
    private readonly IContact _contact;
    //private readonly IAddress address;

    public ContactRepository(IContact contact/*, IAddress address*/)
    {
        _contact = contact;
        //_contact.Address = address;
    }

    /*
private readonly IFileService _fileService;
public ContactRepository(IFileService fileService)
{
   _fileService = fileService;
}*/
    public bool AddToList(IContact _contact)
    {
        try
        {
            _contactList.Add(_contact);
            return true;
        }
        catch { return false; }
    }
    public IEnumerable<IContact> GetAllFromList()
    {
        return _contactList;
    }
}
