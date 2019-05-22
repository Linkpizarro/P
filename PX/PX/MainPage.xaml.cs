using PX.Models;
using PX.ViewModels;
using PX.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PX
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProductoViewModel viewmodel = new ProductoViewModel();
            DetallesProductoView view = new DetallesProductoView();
            viewmodel.Producto = e.SelectedItem as Producto;
            view.BindingContext = viewmodel;
            await Navigation.PushModalAsync(view);
        }
    }
}
