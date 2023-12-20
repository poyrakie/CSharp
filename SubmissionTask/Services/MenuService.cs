using SubmissionTask.Interfaces;
using SubmissionTask.ClassLibrary.Interfaces;

namespace SubmissionTask.Services;
///<summary>
/// Service som ansvarar för att hantera menyer och användarinteraktion i applikationen
/// implementerar interfaces IMenuService
///</summary>
public class MenuService(IContactService contactService) : IMenuService
{
    private readonly IContactService _contactService = contactService;

    /// <summary>
    /// Visar huvudmenyn med alternativ att gå vidare till andra submenyer, eller avsluta programmet.
    /// Användarinputen valideras och lämpliga åtgärder vid registrering av felaktig input vidtas.
    /// </summary>
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
            var answer = Console.ReadKey().Key;
            switch (answer)
            {
                case ConsoleKey.D1:
                    ShowAllContacts();
                    break;
                case ConsoleKey.D2:
                    AddContactMenu();
                    break;
                case ConsoleKey.D0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Write("Invalid input detected. Please try again");
                    Console.ReadKey();
                    break;
            }
        }
    }

    ///<summary>
    /// visar lista över alla kontakter. 
    /// Användaren har möjlighet att inspektera enskild kontakt eller återgå till huvudmenyn
    /// Hanterar användarinput och navigering därefter
    ///</summary>
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

    ///<summary>
    /// Visar detaljerad information om specifik kontakt.
    /// Användaren har möjlighet att ta bort kontakten eller återgå till andra menyer
    ///</summary>
    private void ShowContact(int i)
    {
        while(true)
        {
            MenuTitle("CONTACT");
            if (_contactService.ShowContact(i))
            {
                Console.WriteLine("Would you like to delete this contact(1), return to show all contacts(2) or return to main menu(0)?");
                var answer = Console.ReadKey().Key;
                switch (answer)
                {
                    case ConsoleKey.D1:
                        RemoveContactMenu(i);
                        break;
                    case ConsoleKey.D2:
                        ShowAllContacts();
                        break;
                    case ConsoleKey.D0:
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

    ///<summary>
    /// Visar menyn för att lägga till en kontakt, en guidande text för att förberreda användaren att ha rätt uppgifter om kontakten.
    /// Hanterar kontakttillägget och vidarnavigation
    ///</summary>
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
            var answer = Console.ReadKey().Key;
            switch (answer)
            {
                case ConsoleKey.D1:
                    ShowMainMenu();
                    break;
                case ConsoleKey.D2:
                    break;
                default:
                    Console.Write("Invalid input registered, returning to main menu");
                    Console.ReadKey();
                    ShowMainMenu();
                    break;
            }
        }
    }

    ///<summary>
    /// Visar menyn för att ta bort en kontakt och ger möjlighet att bekräfta raderingen av kontakten.
    /// Hanterar användarinput och utför borttagningen om den bekräftas.
    ///</summary>
    private void RemoveContactMenu(int i)
    {
        MenuTitle("REMOVE CONTACT");
        if (_contactService.ShowContact(i))
        {
            if (_contactService.DeleteContact())
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

    ///<summary>
    /// Hjälpmetod för att visa formatering av menytitel
    ///</summary>
    private void MenuTitle(string menu)
    {
        Console.Clear();
        Console.WriteLine($"\t******{menu}******");
        Console.WriteLine();
    }

    ///<summary>
    /// Hjälpmetod för att visa en lista
    ///</summary>
    private void MenuList(string listItem)
    {
        Console.WriteLine($"-\t{listItem}");
    }

    ///<summary>
    /// Hjälpmetod för att visa en lista med ett motsvarande nummer för switch-cases
    ///</summary>
    private void MenuListForSwitchCase(int i, string listItem)
    {
        int j = i + 1;
        Console.WriteLine($"{j+".", -9} {listItem}");
    }
}
