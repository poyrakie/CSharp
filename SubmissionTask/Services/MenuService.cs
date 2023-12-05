using SubmissionTask.Interfaces;
using SubmissionTask.Models;
using SubmissionTask.Repositories;
using System.Net;
using System.Runtime.CompilerServices;

namespace SubmissionTask.Services;

public class MenuService : IMenuService
{
    private readonly IContact _contact;
    private readonly IAddress _address;
    private readonly ContactRepository _contactRepository;

    public MenuService(ContactRepository contactRepository, IContact contact, IAddress address)
    {
        _contactRepository = contactRepository;
        _contact = contact;
        _contact.Address = address;
    }

    /*public MenuService(Contact contact)
    {
        _contact = contact;
    }*/

    public void ShowMainMenu()
    {
        string[] menu =
        {
            "Show all contacts",
            "Go to add contact menu",
            "Go to update contact menu"
        };
        while (true)
        {
            MenuTitle("MAIN MENU");
            for (int i = 0; i < menu.Length; i++)
            {
                MenuListForSwitchCase(i, menu[i]);
            }
            Console.WriteLine("\t0.\tExit program");
            string answer = Console.ReadLine()!;
            switch (answer)
            {
                case "1":
                    ShowAllContacts();
                    break;
                case "2":
                    AddContactMenu();
                    break;
                case "3":
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input detected. Please try again");
                    Console.ReadKey();
                    break;
            }
        }
    }
    private void ShowAllContacts()
    {
        while (true)
        {
            Console.Clear();
            MenuTitle("CONTACTS MENU");
            foreach(Contact contact in _contactRepository.GetAllFromList())
            {
                Console.WriteLine("\t---------------------------------");
                Console.WriteLine($"\tFull name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"\tEmail-address: {contact.Email}");
                Console.WriteLine($"\tPhone-number: {contact.PhoneNumber}");
                Console.WriteLine("\tAddress:");
                Console.WriteLine($"\t\tHouse: {contact.Address.Road} - {contact.Address.HouseNumber}");
                Console.WriteLine($"\t\tPostal code: {contact.Address.PostalCode}");
                Console.WriteLine($"\t\tCity: {contact.Address.City}");
                Console.WriteLine("\t---------------------------------");
            }
            Console.WriteLine("\tPress any key to return to main menu");
            Console.ReadKey();
            ShowMainMenu();
        }
    }
    private void AddContactMenu() 
    {
        string[] menu =
        {
            "You will need to supply the following information:",
            "A first name",
            "A last name",
            "An email-address",
            "A phone-number",
            "An address",
            "Press any key when you're ready to start."
        };
        string[] successMenu =
        {
            "Return to main menu",
            "Add another contact"
        };
        while (true)
        {
            MenuTitle("ADD CONTACT");
            for (int i = 0; i < menu.Length; i++)
            {
                MenuList(menu[i]);
            }
            Console.ReadKey();
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
                            Console.Clear();
                            MenuTitle("ADD CONTACT");
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
                                                Console.Clear();
                                                MenuTitle("ADD CONTACT");
                                                Console.WriteLine("Contact added successfully");
                                                for (int i = 0; i < successMenu.Length; i++)
                                                {
                                                    MenuListForSwitchCase(i, successMenu[i]);
                                                }
                                                string answer = Console.ReadLine()!;
                                                switch (answer)
                                                {
                                                    case "1":
                                                        ShowMainMenu();
                                                        break;
                                                    case "2":
                                                        break;
                                                    default:
                                                        Console.WriteLine("Invalid input registered, returning to main menu");
                                                        Console.ReadKey();
                                                        ShowMainMenu();
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void MenuTitle(string menu)
    {
        Console.Clear();
        Console.WriteLine($"\t\t******{menu}******");
        Console.WriteLine();
    }
    private void MenuList(string listItem)
    {
        Console.WriteLine($"\t-\t{listItem}");
    }
    private void MenuListForSwitchCase(int i, string listItem)
    {
        Console.WriteLine($"\t{i + 1}. \t{listItem}");
    }
    private void AddContactPrompt(string prompt)
    {
        Console.Write($"\t\tPlease enter the {prompt} of your new contact: ");
    }
}
