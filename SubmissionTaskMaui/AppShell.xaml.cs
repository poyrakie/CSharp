using SubmissionTaskMaui.Views;

namespace SubmissionTaskMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Definierar namn av routes och var de ska leda
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
        }
    }
}
