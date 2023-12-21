using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using System.Collections.ObjectModel;
using Contact = SubmissionTask.ClassLibrary.Models.Contact;

namespace SubmissionTaskMaui.ViewModels;

/// <summary>
/// Viewmodel för EditPage. Funktionalitet för relaycommands. Implementerar Interface IQueryAttributable för kommunikation om object från MainViewModel
/// </summary>
/// <param name="contactRepository">Contact repository används för att kommunicera med fileservice och spara ny kontakt hela vägen till fil</param>
public partial class EditViewModel(IContactRepository contactRepository) : ObservableObject, IQueryAttributable
{
    private readonly IContactRepository _contactRepository = contactRepository;



    [ObservableProperty]
    private IContact contact = new Contact();

    /// <summary>
    /// Metod för att updatera kontakt. Kommunicerar de uppdaterade objektet till contactRepository
    /// Rensar sedan Contact
    /// </summary>
    [RelayCommand]
    private async Task Update()
    {
        _contactRepository.Update(Contact);
        Contact = new Contact();

        await Shell.Current.GoToAsync("//MainPage");
    }

    /// <summary>
    /// Metod för att återvända till Main utan att spara ändringar.
    /// Förväntades kunna tömma cachen genom att resetta contact. Förväntade resultat inte uppnådda
    /// </summary>
    [RelayCommand]
    private async Task Return()
    {
        Contact = new Contact();
        await Shell.Current.GoToAsync("//MainPage");
    }

    /// <summary>
    /// Metod från interfacet IQueryAttributable för att hämta parametrar från MainViewModel
    /// </summary>
    /// <param name="query">Contact</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as IContact)!;
    }

}
