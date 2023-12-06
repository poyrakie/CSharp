using SubmissionTask.Interfaces;
using SubmissionTask.Models;

namespace SubmissionTask.Services;

public class MenuService : IMenuService
{
    private readonly IContactService _contactService;
    private readonly IContact _contact;
    private readonly IAddress _address;
    private readonly IContactRepository _contactRepository;

    public MenuService(IContactService contactService, IContactRepository contactRepository, IContact contact, IAddress address)
    {
        _contactService = contactService;
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
            Console.WriteLine($"{"", -10}{"0.", -10}Exit program");
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
            _contactService.ShowAllContacts();
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
            if (_contactService.AddToList())
            {
                Console.WriteLine("Contact added successfully.");
            }
            else
            {
                Console.WriteLine("Something went wrong. Please try again");
            }
            Console.ReadKey();
            Console.Clear();
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
        int j = i + 1;
        Console.WriteLine($"{"", -10}{j+".", -9} {listItem}");
    }
}
