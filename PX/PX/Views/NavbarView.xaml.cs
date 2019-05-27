using PX.Models;
using PX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavbarView : MasterDetailPage
    {
        public List<ItemMenuPage> Items;
        public NavbarView()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)))
            {
                BarBackgroundColor = Color.FromHex("#4528A2")
            };
            IsPresented = false;
            this.Menu.ItemSelected += ChangePage;
        }
        private async void ChangePage(object sender, SelectedItemChangedEventArgs e)
        {
            ItemMenuPage select = (ItemMenuPage)e.SelectedItem;
            if(select.Title == "Logout")
            {
                App.Locator.SessionService.Usuario = null;
                App.Locator.SessionService.Cadena = null;
                App.Locator.SessionService.Articulos = null;
                MessagingCenter.Send<ItemsMenuViewModel>(App.Locator.ItemsMenuViewModel, "login");
            }
            else
            {
                if(select.Title == "Login")
                {
                    await Navigation.PushModalAsync(new LoginView());
                    IsPresented = false;
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(select.TypePage))
                    {
                        BarBackgroundColor = Color.FromHex("#4528A2")
                    };
                    IsPresented = false;
                }

            }
           
        }

        private async void Carrito_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CarritoView());
        }
    }
}