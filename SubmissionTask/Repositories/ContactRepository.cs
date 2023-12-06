using Newtonsoft.Json;
using SubmissionTask.Interfaces;
using System.Diagnostics;

namespace SubmissionTask.Repositories;

public class ContactRepository(IContact contact, IFileService fileService) : IContactRepository
{
    private List<IContact> _contactList = [];
    private readonly IContact _contact = contact;
    private readonly IFileService _fileService = fileService;

    public bool AddToList(IContact _contact)
    {
        try
        {
            if (! _contactList.Any(x => x.Email == _contact.Email)) 
            {
                _contactList.Add(_contact);

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
            }
            return _contactList;
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return null!;
    }
}
