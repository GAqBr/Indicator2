using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Circular Std Font.otf", Alias = "Circular Std")]
[assembly: ExportFont("Monstserrat-VariableFont_wght.ttf", Alias = "Monstserrat")]
[assembly: ExportFont("Poppins-Regular.ttf", Alias = "Poppins" )]
[assembly: ExportFont("Poppins-Italic.ttf", Alias = "Poppins-Italic")]
[assembly: ExportFont("Poppins-Bold.ttf", Alias = "Poppins-Bold")]
namespace Indicator2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
