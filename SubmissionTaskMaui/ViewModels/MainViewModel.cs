using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using System.Collections.ObjectModel;

namespace SubmissionTaskMaui.ViewModels;

/// <summary>
/// Viewmodel för MainPage. Funktionalitet för relaycommands.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    /// <summary>
    /// Konstruktor som skapar en observableCollection
    /// Initierar även UpdateContactList för att dynamiskt kunna updatera gränssnittet
    /// </summary>
    /// <param name="contactRepository"></param>
    public MainViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        _contacts = new ObservableCollection<IContact>(_contactRepository.GetAllFromList());
        UpdateContactList();    
    }

    [ObservableProperty]
    private ObservableCollection<IContact> _contacts = [];

    /// <summary>
    /// Metod för att navigera till AddPage RelayCommand
    /// </summary>
    [RelayCommand]
    private async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("AddPage");
    }

    /// <summary>
    /// Metod för relaycommand att navigera till Edit.
    /// Avnänder ShellNaviagionQueryParameters för att skicka med parametrar av kontakten som ska editeras
    /// </summary>
    [RelayCommand]
    private async Task NavigateToEdit(IContact contact) 
    {
        var parameters = new ShellNavigationQueryParameters
        {
            { "Contact", contact }
        };

        await Shell.Current.GoToAsync("EditPage", parameters);
    }

    /// <summary>
    /// Metod för relaycommand att ta bort en kontakt.
    /// Kommunicerar med ContactRepository för att spara till fil och uppdatera listan
    /// </summary>
    /// <param name="contact">kontakten som ska tas bort</param>
    [RelayCommand]
    private void Remove(IContact contact)
    {
        _contactRepository.RemoveFromList(contact.Email);
    }

    /// <summary>
    /// Metod för att initiera eventhandler från contactRepository
    /// </summary>
    private void UpdateContactList()
    {
        _contactRepository.ContactListUpdated += (object? sender, EventArgs e) =>
        {
            Contacts = new ObservableCollection<IContact>(_contactRepository.GetAllFromList());
        };
    }
}
