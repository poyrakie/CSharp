using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using System.Collections.ObjectModel;
using Contact = SubmissionTask.ClassLibrary.Models.Contact;

namespace SubmissionTaskMaui.ViewModels;

public partial class EditViewModel(IContactRepository contactRepository) : ObservableObject, IQueryAttributable
{
    private readonly IContactRepository _contactRepository = contactRepository;



    [ObservableProperty]
    private IContact contact = new Contact();

    [RelayCommand]
    private async Task Update()
    {
        _contactRepository.Update(Contact);
        Contact = new Contact();

        await Shell.Current.GoToAsync("//MainPage");
    }

    [RelayCommand]
    private async Task Return()
    {
        Contact = new Contact(); // förväntade mig att detta skulle cleara cachen för editsidan. Om man returnar o sen går in i edit igen så är dock alla ändringar sparade
        await Shell.Current.GoToAsync("//MainPage");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as IContact)!;
    }

}
