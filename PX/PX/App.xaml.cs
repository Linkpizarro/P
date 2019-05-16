using PX.Configuration;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PX
{
    public partial class App : Application
    {
        private static IoCConfiguration _Locator;

        //UNA PROPIEDAD QUE NOS DEVUELVA UN new IoCConfiguration
        //O QUE RECUPERE EL QUE YA TENGAMOS CREADO
        public static IoCConfiguration Locator
        {
            get
            {
                //NOTA: devuelve el que se tenga o esa una instancia de un nuevo objeto de tipo "IoCConfiguration"
                return _Locator = _Locator ?? new IoCConfiguration();
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
