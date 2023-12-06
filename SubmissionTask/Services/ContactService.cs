using SubmissionTask.Interfaces;
using SubmissionTask.Repositories;
using System.Diagnostics;

namespace SubmissionTask.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IContact _contact;
    public ContactService(IContactRepository contactRepository, IContact contact)
    {
        _contactRepository = contactRepository;
        _contact = contact;
    }
    public bool AddToList()
    {
        try
        {
            AddContactPrompt("first name");
            _contact.FirstName = Console.ReadLine()!.Trim();
            if (!string.IsNullOrEmpty(_contact.FirstName))
            {
                _contact.FirstName = char.ToUpper(_contact.FirstName[0]) + _contact.FirstName.Substring(1);
                AddContactPrompt("last name");
                _contact.LastName = Console.ReadLine()!.Trim();
                if (!string.IsNullOrEmpty(_contact.LastName))
                {
                    _contact.LastName = char.ToUpper(_contact.LastName[0]) + _contact.LastName.Substring(1);
                    AddContactPrompt("email-address");
                    _contact.Email = Console.ReadLine()!.Trim();
                    if (!string.IsNullOrEmpty(_contact.Email))
                    {
                        AddContactPrompt("phone-number");
                        _contact.PhoneNumber = Console.ReadLine()!.Trim();
                        if (!string.IsNullOrEmpty(_contact.PhoneNumber))
                        {
                            Console.WriteLine();
                            Console.WriteLine("\t\tSo far i've gotten");
                            Console.WriteLine($"\t\tFirst name: {_contact.FirstName}");
                            Console.WriteLine($"\t\tLast name: {_contact.LastName}");
                            Console.WriteLine($"\t\tEmail-address: {_contact.Email}");
                            Console.WriteLine("\t\tLets Continue with the address of your new contact");
                            AddContactPrompt("city");
                            _contact.Address.City = Console.ReadLine()!.Trim();
                            if (!string.IsNullOrEmpty(_contact.Address.City))
                            {
                                _contact.Address.City = char.ToUpper(_contact.Address.City[0]) + _contact.Address.City.Substring(1);
                                AddContactPrompt("road");
                                _contact.Address.Road = Console.ReadLine()!.Trim();
                                if (!string.IsNullOrEmpty(_contact.Address.Road))
                                {
                                    _contact.Address.Road = char.ToUpper(_contact.Address.Road[0]) + _contact.Address.Road.Substring(1);
                                    AddContactPrompt("house number");
                                    _contact.Address.HouseNumber = Console.ReadLine()!.Trim();
                                    if (!string.IsNullOrEmpty(_contact.Address.HouseNumber))
                                    {
                                        AddContactPrompt("postal code");
                                        string userInput = Console.ReadLine()!.Trim();
                                        if (int.TryParse(userInput, out int result))
                                        {
                                            _contact.Address.PostalCode = result;
                                            if (_contactRepository.AddToList(_contact))
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteContact()
    {
        throw new NotImplementedException();
    }

    public bool ShowAllContacts()
    {
        try
        {
            foreach (IContact contact in _contactRepository.GetAllFromList())
            {
                Console.WriteLine($"{"", -10}----------------------------");
                Console.WriteLine($"{"",-10}{"Contact name: ", -10}{contact.FirstName} {contact.FirstName}");
                Console.WriteLine($"{"",-10}----------------------------\n");
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool UpdateContact()
    {
        throw new NotImplementedException();
    }
    private void AddContactPrompt(string prompt)
    {
        Console.Write($"\t\tPlease enter the {prompt} of your new contact: ");
    }
}
