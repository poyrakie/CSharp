using Newtonsoft.Json;
using SubmissionTask.Interfaces;
using SubmissionTask.Models;
using System.Diagnostics;

namespace SubmissionTask.Repositories;

public class ContactRepository : IContactRepository
{
    private List<IContact> _contactList;
    private readonly IFileService _fileService;

    public ContactRepository(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = GetAllFromList().ToList();
    }

    public bool AddToList(IContact contact)
    {
        try
        {
            if (! _contactList.Any(x => x.Email == contact.Email)) 
            {
                _contactList.Add(contact);

                var json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });

                _fileService.SaveToFile(json);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex);
            return false; 
        }
    }
    public IEnumerable<IContact> GetAllFromList()
    {
        try
        {
            var content = _fileService.LoadFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                _contactList = JsonConvert.DeserializeObject<List<IContact>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                })!;
                return _contactList;
            }
            else
            {
                List<IContact> _contactList = [];
                return _contactList;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return null!;
    }

    public bool RemoveFromList(string email, int i)
    {
        try
        {
            IContact contactToRemove = _contactList[i];
            if (contactToRemove != null)
            {
                if (contactToRemove.Email == email)
                {
                    _contactList.RemoveAt(i);
                    var json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                    });
                    _fileService.SaveToFile(json);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public bool ScanListForEmail(string email)
    {
        try
        {
            if (_contactList.Any(x => x.Email == email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
