using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubmissionTask.ClassLibrary.Interfaces;
using Contact = SubmissionTask.ClassLibrary.Models.Contact;

namespace SubmissionTaskMaui.ViewModels;

public partial class EditViewModel : ObservableObject, IQueryAttributable
{


    [ObservableProperty]
    private IContact contact = new Contact();

    [RelayCommand]
    private async Task Update()
    {
        //_contactMauiService.Update(Contact);
        Contact = new Contact();

        await Shell.Current.GoToAsync("//MainPage");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as IContact)!;
    }
}
