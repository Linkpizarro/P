﻿using PX.Base;
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
            this.UserName = "";
            MessagingCenter.Subscribe<ItemsMenuViewModel>(this, "login", (sender) => {
               this.LoadPages();
               this.UserName = App.Locator.SessionService.Usuario.Username;
            });
        }
        private void LoadPages()
        {
            this.Items = new ObservableCollection<ItemMenuPage>();
            this.Items.Clear();
            var home = new ItemMenuPage() { Title = "Home", TypePage = typeof(MainPage) };
            this.Items.Add(home);
            var products = new ItemMenuPage() { Title = "Productos", TypePage = typeof(ProductosView) };
            this.Items.Add(products);
            if (App.Locator.SessionService.Cadena == null)
            {
                var login = new ItemMenuPage() { Title = "Login", TypePage = typeof(LoginView) };
                this.Items.Add(login);
            }
            else
            {   
                var orders = new ItemMenuPage() { Title = "Tus Pedidos", TypePage = typeof(ComprasUsuarioView) };
                this.Items.Add(orders);
                var logout = new ItemMenuPage() { Title = "Logout", TypePage = typeof(String) };
                this.Items.Add(logout);
            }
           
        }
        private ObservableCollection<ItemMenuPage> _Items;
        public ObservableCollection<ItemMenuPage> Items
        {
            get { return this._Items; }
            set { this._Items = value; OnPropertyChange("Items"); }
        }
        private String _UserName;
        public String UserName
        {
            get { return this._UserName; }
            set { this._UserName = value; OnPropertyChange("UserName"); }
        }
    }
}
