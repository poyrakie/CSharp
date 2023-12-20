using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using System.Collections.ObjectModel;

namespace SubmissionTaskMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public MainViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        _contacts = new ObservableCollection<IContact>(_contactRepository.GetAllFromList());
        UpdateContactList();    
    }

    [ObservableProperty]
    private ObservableCollection<IContact> _contacts = [];

    [RelayCommand]
    private async Task NavigateToAdd(IContact contact)// Kolla upp om jag behöver parametrarna
    {
        await Shell.Current.GoToAsync("AddPage");
    }

    [RelayCommand]
    private async Task NavigateToEdit(IContact contact) 
    {
        var parameters = new ShellNavigationQueryParameters
        {
            { "Contact", contact }
        };

        await Shell.Current.GoToAsync("EditPage", parameters);
    }

    [RelayCommand]
    private void Remove(IContact contact)
    {
        _contactRepository.RemoveFromList(contact.Email);
    }

    private void UpdateContactList()
    {
        _contactRepository.ContactListUpdated += (object? sender, EventArgs e) =>
        {
            Contacts = new ObservableCollection<IContact>(_contactRepository.GetAllFromList());
        };
    }
}
