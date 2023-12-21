using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using Contact = SubmissionTask.ClassLibrary.Models.Contact;

namespace SubmissionTaskMaui.ViewModels;

/// <summary>
/// Viewmodel för AddPage. Funktionalitet för relaycommands
/// </summary>
/// <param name="contactRepository">Contact repository används för att kommunicera med fileservice och spara ny kontakt hela vägen till fil</param>
public partial class AddViewModel(IContactRepository contactRepository) : ObservableObject
{
    private readonly IContactRepository _contactRepository = contactRepository;

    [ObservableProperty]
    private IContact contact = new Contact();
    
    /// <summary>
    /// Metod för att lägga till kontakt i listan som skickar vidare objektet till contactRepository
    /// Rensar sedan Contact
    /// </summary>
    [RelayCommand]
    private async Task Add()
    {
        _contactRepository.AddToList(Contact);
        Contact = new Contact();
        
        await Shell.Current.GoToAsync("//MainPage");
    }


    /// <summary>
    /// Metod för att återvända till huvudmenyn
    /// </summary>
    [RelayCommand]
    private async Task Return()
    {
        Contact = new Contact();
        await Shell.Current.GoToAsync("//MainPage");
    }
}
