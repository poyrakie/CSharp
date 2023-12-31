﻿using Newtonsoft.Json;
using SubmissionTask.ClassLibrary.Interfaces;
using System.Diagnostics;

namespace SubmissionTask.ClassLibrary.Repositories;

///<summary>
/// En repository för contacts som implementerar interface IContactRepository.
/// Ansvarar för att lägga till, hämta, ta bort och skanna kontakter från Fileservice.
///</summary>
public class ContactRepository : IContactRepository
{
    private List<IContact> _contactList;
    private readonly IFileService _fileService;
    private readonly string _filePath = @"C:\Programmering\EC\CSharp\SubmissionTask\content.json";
    // Eventhandler för att kommunicera med maui app vid förändringar i lista
    public event EventHandler? ContactListUpdated;

    ///<summary>
    /// Konstruktor som tar emot IFileService för filhantering
    /// initialerar kontaktlistan genom att hämta befintliga kontakter från fileservice
    ///</summary>
    public ContactRepository(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = GetAllFromList().ToList();
    }

    ///<summary>
    /// Lägger till en kontakt i listan om kontaktens email inte redan finns.
    /// Uppdaterar filen och returnerar bool motsvarande operationens framgång
    ///</summary>
    public bool AddToList(IContact contact)
    {
        try
        {
            if (!_contactList.Any(x => x.Email == contact.Email))
            {
                _contactList.Add(contact);

                var json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });

                _fileService.SaveToFile(json, _filePath);
                ContactListUpdated?.Invoke(this, EventArgs.Empty);
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

    ///<summary>
    /// Hämtar alla kontakter från fileservice och returnerar som IEnumerable lista.
    /// Returnar null om fil saknas.
    ///</summary>
    public IEnumerable<IContact> GetAllFromList()
    {
        try
        {
            var content = _fileService.LoadFromFile(_filePath);
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

    ///<summary>
    /// Tar bort en kontakt från listan baserat på e-postadress och index. 
    /// Uppdaterar lagringsplatsen och returnerar bool motsvarande operationens framgång
    ///</summary>
    public bool RemoveFromList(string email)
    {
        try
        {
            IContact contactToRemove = _contactList.FirstOrDefault(x => x.Email == email)!;
            if (contactToRemove != null)
            {
                if (contactToRemove.Email == email)
                {
                    _contactList.Remove(contactToRemove);
                    var json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                    });
                    _fileService.SaveToFile(json, _filePath);
                    ContactListUpdated?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    /// <summary>
    /// try/catch metod för att ta bort kontakt från listan. Sparar sen den nya listan till fil, invoke för att kommunicera med maui app att omrenderas
    /// </summary>
    /// <param name="contact">Contact som ska tas bort</param>
    /// <returns>bool motsvarande operationens framgång</returns>
    public bool Update(IContact contact)
    {
        try
        {
            IContact contactToUpdate = _contactList.FirstOrDefault(x => x.Email == contact.Email)!;
            if (contactToUpdate != null)
            {
                contactToUpdate = contact;
                var json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
                _fileService.SaveToFile(json, _filePath);
                ContactListUpdated?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    ///<summary>
    /// Skannar listan för att kontrollera om en kontakt med angiven e-postadress finns.
    /// Returnerar true om kontakten finns, annars false.
    ///</summary>
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
