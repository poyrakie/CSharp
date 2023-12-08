using SubmissionTask.Interfaces;
using SubmissionTask.Models;

namespace SubmissionTask.Services;

public class MenuService(IContactService contactService) : IMenuService
{
    private readonly IContactService _contactService = contactService;

    public void ShowMainMenu()
    {
        string[] menu =
        {
            "Show all contacts",
            "Go to add contact menu"
        };
        while (true)
        {
            MenuTitle("MAIN MENU");
            for (int i = 0; i < menu.Length; i++)
            {
                MenuListForSwitchCase(i, menu[i]);
            }
            Console.WriteLine($"{"0.", -10}Exit program");
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
                    Console.Write("Invalid input detected. Please try again");
                    Console.ReadKey();
                    break;
            }
        }
    }
    private int ShowAllContacts()
    {
        while (true)
        {
            Console.Clear();
            MenuTitle("CONTACTS MENU");
            _contactService.ShowAllContacts();
            Console.WriteLine("Return to main menu(0)");
            Console.WriteLine("Inspect a contact (number before contact)");
            string answer = Console.ReadLine()!;
            if (answer == "0")
                ShowMainMenu();
            else if (int.TryParse(answer, out int i))
            {
                i--;
                ShowContact(i);
            }
            else
                Console.Write("Invalid input registered. Please try again");
                
        }
    }
    private void ShowContact(int i)
    {
        while(true)
        {
            MenuTitle("CONTACT");
            if (_contactService.ShowContact(i))
            {
                Console.WriteLine("Would you like to delete this contact(1), return to show all contacts(2) or return to main menu(0)?");
                string answer = Console.ReadLine()!;
                switch (answer)
                {
                    case "1":
                        RemoveContactMenu(i);
                        break;
                    case "2":
                        ShowAllContacts();
                        break;
                    case "0":
                        ShowMainMenu();
                        break;
                    default:
                        Console.Write("Invalid input detected, please try again.");
                        Console.ReadKey();
                        break;
                }
                
            }
            else
            {
                Console.Write("Invalid input registered. Returning to Contacts menu.");
                Console.ReadKey();
                ShowAllContacts();
            }
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
                Console.Write("Contact added successfully.");
            }
            else
            {
                Console.Write("Something went wrong. Please try again");
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
                    Console.Write("Invalid input registered, returning to main menu");
                    Console.ReadKey();
                    ShowMainMenu();
                    break;
            }
        }
    }
    private void RemoveContactMenu(int i)
    {
        MenuTitle("REMOVE CONTACT");
        if (_contactService.ShowContact(i))
        {
            if (_contactService.DeleteContact(i))
            {
                Console.ReadKey();
                ShowAllContacts();
            }
            Console.ReadKey();
            ShowAllContacts();
        }
        else 
        {
            Console.Write("Something went wrong, returning to show all contacts");
            Console.ReadKey();
            ShowAllContacts();
        }
    }
    private void MenuTitle(string menu)
    {
        Console.Clear();
        Console.WriteLine($"\t******{menu}******");
        Console.WriteLine();
    }
    private void MenuList(string listItem)
    {
        Console.WriteLine($"-\t{listItem}");
    }
    private void MenuListForSwitchCase(int i, string listItem)
    {
        int j = i + 1;
        Console.WriteLine($"{j+".", -9} {listItem}");
    }
}
