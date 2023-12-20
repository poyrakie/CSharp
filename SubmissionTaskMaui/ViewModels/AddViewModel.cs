using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using Contact = SubmissionTask.ClassLibrary.Models.Contact;

namespace SubmissionTaskMaui.ViewModels;

public partial class AddViewModel(IContactRepository contactRepository) : ObservableObject
{
    private readonly IContactRepository _contactRepository = contactRepository;

    [ObservableProperty]
    private IContact contact = new Contact();

    [RelayCommand]
    private async Task Add()
    {
        _contactRepository.AddToList(Contact);
        Contact = new Contact();
        
        await Shell.Current.GoToAsync("//MainPage");
    }
}
