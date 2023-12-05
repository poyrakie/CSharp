using SubmissionTask.Interfaces;
using SubmissionTask.Repositories;

namespace SubmissionTask.Services;

public class ContactService : IContactService
{
    private readonly ContactRepository _contactRepository;

    public ContactService(ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public bool AddToList()
    {
        throw new NotImplementedException();
    }

    public bool DeleteContact()
    {
        throw new NotImplementedException();
    }

    public bool ShowAllContact()
    {
        throw new NotImplementedException();
    }

    public bool UpdateContact()
    {
        throw new NotImplementedException();
    }
}
