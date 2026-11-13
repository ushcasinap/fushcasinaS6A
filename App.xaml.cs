using Microsoft.Extensions.DependencyInjection;

namespace fushcasinaS6A
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Views.vEstudiante()));
        }
    }
}