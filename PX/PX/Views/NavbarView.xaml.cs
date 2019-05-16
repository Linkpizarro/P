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
            this.Items = new List<ItemMenuPage>();
            this.LoadPages();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage))) {
                BarBackgroundColor = Color.FromHex("#4528A2")
            };
            IsPresented = false;
        }
        private void LoadPages()
        {
            var home = new ItemMenuPage() { Title = "Home",TypePage= typeof(MainPage) };
            var login = new ItemMenuPage() { Title = "Login", TypePage = typeof(LoginView) };
            this.Items.Add(home);
            this.Items.Add(login);
            this.Menu.ItemsSource = this.Items;
        }
    }
}