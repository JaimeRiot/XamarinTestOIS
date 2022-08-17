using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTest160822.Model;
using XamarinTest160822.Services;
using XamarinTest160822.View.Products;
using XamarinTest160822.View.Register;

namespace XamarinTest160822.ViewModel.UserViewModel
{
    public class UserViewModel: BaseViewModel.BaseViewModel
    {
        #region Attributes
        private string name;
        private string lastName;
        private string email;
        private string password;
        private ApiService apiServices;
        #endregion
        #region Properties
        DataServices service = new DataServices();
        public string Name
        {
            get => name;
            set => SetValue(ref name, value);
        }
        public string LastName
        {
            get => lastName;
            set => SetValue(ref lastName, value);
        }
        public string Email
        {
            get => email;
            set => SetValue(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetValue(ref password, value);
        }

        private User user;
        public User User
        {
            get => user;
            set => SetValue(ref user, value);
        }
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get => users;
            set => SetValue(ref users, value);
        }
        #endregion
        public UserViewModel()
        {
            NavigationPop = new Command(async () => await NavigationEvent("Pop"));
            NavigationPush = new Command(async () => await NavigationEvent("Push"));
            RegisterCommand = new Command(async () => await RegisterUser());
            LoginCommand = new Command(async () => await LoginUser());
        }

        private async Task LoginUser()
        {
            IsRefresh = true;
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Correo", "El correo no puede quedar vacio", "Aceptar");
                IsRefresh = false;
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Contraseña", "La contraseña no puede quedar vacio", "Aceptar");
                IsRefresh = false;
                return;
            }
            var result = await service.FindUser(Email, Password);
            if (result)
            {
                var instanceProduct = MainViewModel.MainViewModel.GetInstance().ProductsViewModel = new ProductsViewModel.ProductsViewModel();
                if (instanceProduct != null)
                {
                    IsRefresh = false;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsPage());
                }
                IsRefresh = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Usuario No Registrado", "Este usuario no se encuentra en nuestra base de datos", "Aceptar");
                IsRefresh = false;
                return;
            }
        }

        public async Task NavigationEvent(string option)
        {
            if(option =="Pop")
                await Application.Current.MainPage.Navigation.PopModalAsync();
            else
                await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());

        }
        private async Task RegisterUser()
        {
            IsRefresh = true;
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Nombre", "El nombre no puede quedar vacio", "Aceptar");
                IsRefresh = false;
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert("Apellido", "El apellido no puede quedar vacio", "Aceptar");
                IsRefresh =false;
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Email", "El email no puede quedar vacio", "Aceptar");
                IsRefresh=false;
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Contraseña", "La contraseña no puede quedar vacio", "Aceptar");
                IsRefresh = false;
                return;
            }
            else
            {
                var findUser = await FindUser();
                if (!findUser)
                {
                    await SaveUserToDB();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Usuario Registrado", "Este usuario ya fue registrado anteriormente", "Aceptar");
                    IsRefresh = false;
                    return;
                }
                var instance = MainViewModel.MainViewModel.GetInstance().ProductsViewModel = new ProductsViewModel.ProductsViewModel();
                if(instance != null)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsPage());
                    IsRefresh = false;
                }
                else
                {
                    IsRefresh = false;
                    await Application.Current.MainPage.DisplayAlert("Ocurrio un error", "Ocurrio un error al cargar la lista", "Aceptar");
                }
                IsRefresh = false;
                return;
            }
        }
        private async Task<bool> FindUser()
        {
            var getUser = await service.FindUser(Email, Password);
            return getUser;
        }
        public async Task SaveUserToDB()
        {
            User = new User()
            {
                Email = Email,
                LastName = LastName,
                Name = Name,
                Password = Password
            };
            await service.Insert(User);
        }

        public ICommand NavigationPop { get; set; }
        public ICommand NavigationPush { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
    }
}
