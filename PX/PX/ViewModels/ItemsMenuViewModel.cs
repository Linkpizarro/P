using PX.Base;
using PX.Models;
using PX.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PX.ViewModels
{
    public class ItemsMenuViewModel : ViewModelBase
    {
        public ItemsMenuViewModel()
        {
            this.LoadPages();
            MessagingCenter.Subscribe<ItemsMenuViewModel>(this, "login", (sender) => {
               this.LoadPages();
            });
        }
        private void LoadPages()
        {
            this.Items = new ObservableCollection<ItemMenuPage>();
            this.Items.Clear();
            var home = new ItemMenuPage() { Title = "Home", TypePage = typeof(MainPage) };
            var products = new ItemMenuPage() { Title = "Productos", TypePage = typeof(ProductosView) };
            if (App.Locator.SessionService.Cadena == null)
            {
                var login = new ItemMenuPage() { Title = "Login", TypePage = typeof(LoginView) };
                this.Items.Add(login);
            }
            this.Items.Add(home);
            this.Items.Add(products);
        }
        private ObservableCollection<ItemMenuPage> _Items;
        public ObservableCollection<ItemMenuPage> Items
        {
            get { return this._Items; }
            set { this._Items = value; OnPropertyChange("Items"); }
        }
    }
}
