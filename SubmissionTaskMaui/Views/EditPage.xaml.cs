using SubmissionTaskMaui.ViewModels;

namespace SubmissionTaskMaui.Views;

public partial class EditPage : ContentPage
{
	public EditPage(EditViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}