using SubmissionTask.Interfaces;
using SubmissionTask.Models;
using SubmissionTask.Repositories;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace SubmissionTask.Services;

///<summary>
/// Service för hantering av kontakter och använder interface IContactService.
/// Ansvarar för att lägga till, visa, ta bort och visa detaljer för kontakter,
/// samt utföra validering av användarinput
///</summary>
public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;

    ///<summary>
    /// Lägger till en ny kontakt i systemet genom att guida användaren genom att ange giltig information för varje kontaktattribut. 
    /// Returnerar bool motsvarande operationens framgång.
    ///</summary>
    public bool AddToList()
    {
        string[] propertyName =
        {
            "First Name",
            "Last Name",
            "Email-address",
            "Phone-number",
            "City",
            "Road",
            "Housenumber",
            "Postalcode"
        };
        string[] regexPattern =
        {
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            @"^\d{5}(?:-\d{5})?$"
        };
        IContact contact = new Contact();
        try
        {
            SetValidInput(() => Console.ReadLine()?.Trim()!, val => contact.FirstName = val, propertyName[0]);
            SetValidInput(() => Console.ReadLine()?.Trim()!, val => contact.LastName = val, propertyName[1]);
            SetValidInputForEmail(() => Console.ReadLine()?.Trim()!, val => contact.Email = val, propertyName[2], regexPattern[0]);
            SetValidInput(() => Console.ReadLine()?.Trim()!, val => contact.PhoneNumber = val, propertyName[3]);

            Console.WriteLine("\nSo far, you've entered:");
            Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Email: <{contact.Email}>");
            Console.WriteLine($"Phone number: {contact.PhoneNumber}\n");
            
            SetValidInput(() => Console.ReadLine()?.Trim()!, val => contact.City = val, propertyName[4]);
            SetValidInput(() => Console.ReadLine()?.Trim()!, val => contact.Road = val, propertyName[5]);
            SetValidInput(() => Console.ReadLine()?.Trim()!, val => contact.HouseNumber = val, propertyName[6]);
            SetValidInputWithRegex(() => Console.ReadLine()?.Trim()!, val => contact.PostalCode = val, propertyName[7], regexPattern[1]);

            if (_contactRepository.AddToList(contact))
            {
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
    /// Tar bort en befintlig kontakt baserat på användarinput och kontaktindex.
    /// Bekräftas med användarinput för Email.
    /// Returnerar bool motsvarande operationens framgång
    ///</summary>
    public bool DeleteContact(int i)
    {
        try
        {
            Console.Write("Are you sure you want to delete this contact(press y to confirm)? ");
            var input = Console.ReadKey().Key;
            if(input == ConsoleKey.Y)
            {
                Console.Write("\nPlease confirm by typing the email of the contact: ");
                string email = Console.ReadLine()!;
                if(_contactRepository.RemoveFromList(email, i))
                {
                    Console.Write("Contact was deleted succesfully, returning to show all contacts ");
                    return true;
                }
                else
                {
                    Console.Write("Email is not a match for the contact. Returning to show all contacts ");
                    return false;
                }
            }
            else
            {
                Console.Write("Returning to show all contacts");
                return false;
            }
        }
        catch (Exception ex){ Debug.WriteLine(ex.Message); }
        return false;
    }

    ///<summary>
    /// Visar en lista över alla befintliga kontakter.
    /// Returnerar bool motsvarande operationens framgång
    ///</summary>
    public bool ShowAllContacts()
    {
        try
        {
            int i = 1;
            foreach (IContact contact in _contactRepository.GetAllFromList())
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"{i+"."}{"Contact name: ", -10}{contact.FirstName} {contact.LastName}");
                Console.WriteLine("-----------------------------------\n");
                i++;
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    ///<summary>
    /// Visar detaljer för en specifik kontakt baserat på kontaktindex.
    /// Returnerar bool motsvarande operationens framgång
    ///</summary>
    public bool ShowContact(int i)
    {
        try
        {
            List<IContact> contactList = _contactRepository.GetAllFromList().ToList();
            IContact contact = contactList[i];
            Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Email: <{contact.Email}>");
            Console.WriteLine($"Phonenumber: {contact.PhoneNumber}");
            Console.WriteLine($"{"",-5}Address: ");
            Console.WriteLine($"{"",-10}House: {contact.Road} {contact.HouseNumber}");
            Console.WriteLine($"{"",-10}Postalcode: {contact.PostalCode}");
            Console.WriteLine($"{"",-10}City: {contact.City}");
            return true;
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    ///<summary>
    /// Hjälpmetod för att ange giltig information för ett kontaktattribut.
    ///</summary>
    private void SetValidInput(Func<string> getProperty, Action<string> setProperty, string property)
    {
        while (true)
        {
            Console.Write($"{property}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty( input ) )
            {
                input = char.ToUpper(input[0]) + input.Substring(1);
                setProperty( input );
                break;
            }
            else
            {
                Console.WriteLine($"{property} may not be empty");
            }
        }
    }

    ///<summary>
    /// Hjälpmetod för att ange giltig information för ett kontaktattribut med användning av regex.
    ///</summary>
    private void SetValidInputWithRegex(Func<string> getProperty, Action<string> setProperty, string property, string regexPattern)
    {
        while (true)
        {
            string _regex = regexPattern;
            Console.Write($"{property}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                Regex regex = new(_regex);
                bool isMatch = regex.IsMatch( input );
                if (isMatch)
                {
                    setProperty( input );
                    break;
                }
                else
                {
                    Console.WriteLine($"{input} is not a valid {property}");
                }
            }
            else
            {
                Console.WriteLine($"{property} may not be empty");
            }
        }
    }

    ///<summary>
    /// Hjälpmetod för att ange giltig e-postadress och kontrollera om den redan finns i systemet.
    ///</summary>
    private void SetValidInputForEmail(Func<string> getProperty, Action<string> setProperty, string property, string regexPattern)
    {
        while (true)
        {
            string _regex = regexPattern;
            Console.Write($"{property}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                Regex regex = new(_regex);
                bool isMatch = regex.IsMatch(input);
                if (isMatch)
                {
                    if (_contactRepository.ScanListForEmail(input))
                    {
                        Console.WriteLine($"{input} already exsists, email must be unique");
                    }
                    else
                    {
                        setProperty(input);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"{input} is not a valid {property}");
                }
            }
            else
            {
                Console.WriteLine($"{property} may not be empty");
            }
        }
    }
}
