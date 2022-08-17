using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest160822.View.Login;
using XamarinTest160822.View.Products;
using XamarinTest160822.ViewModel.MainViewModel;

namespace XamarinTest160822
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainViewModel.GetInstance().UserViewModel = new ViewModel.UserViewModel.UserViewModel();
            MainPage = new LoginPage();
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
