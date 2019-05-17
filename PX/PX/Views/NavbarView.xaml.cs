using PX.Models;
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
        private void ChangePage(object sender, SelectedItemChangedEventArgs e)
        {
            ItemMenuPage select = (ItemMenuPage)e.SelectedItem;
            Detail = new NavigationPage((Page)Activator.CreateInstance(select.TypePage)) {
                BarBackgroundColor = Color.FromHex("#4528A2")
            };
            IsPresented = false;
        }
    }
}