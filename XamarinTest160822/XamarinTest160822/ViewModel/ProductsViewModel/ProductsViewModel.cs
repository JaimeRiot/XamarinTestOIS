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

namespace XamarinTest160822.ViewModel.ProductsViewModel
{
    public class ProductsViewModel : BaseViewModel.BaseViewModel
    {
        private ApiService apiService;
        private DataServices data;
        private Product product;

        public Product Product
        {
            get => product;
            set => SetValue(ref product, value);
        }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get => selectedProduct;
            set 
            { 
                if(selectedProduct != value)
                {
                    selectedProduct = value;
                    OnPropertyChanged();
                    NavigationDetail();
                }
            }
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetValue(ref products, value);
        }
        public ProductsViewModel()
        {
            apiService = new ApiService();
            data = new DataServices();
            LoadProducts();
        }
        public ProductsViewModel(Product product)
        {
            Product = product;
        }

        private async void LoadProducts()
        {
            IsRefresh = true;
            var connection = apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await LoadProductsFromAPI();
                if (answer)
                {
                    await SaveProductsToLocalDB();
                }      
            }
            else
            {
                await LoadProductsFromLocalDB(); 
            }
            if(Products == null || Products.Count == 0)
            {
                if (!connection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Conectese a internet para descargar la lista de productos por primera vez", "Aceptar");
                    IsRefresh = false;
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error al cargar los productos", "Aceptar");
                    IsRefresh = false;
                    return;
                }  
            }
            IsRefresh = false;
        }

        private async Task LoadProductsFromLocalDB()
        {
            var list = await data.GetAllProducts();
            this.Products = new ObservableCollection<Product>(list);
        }

        private async Task SaveProductsToLocalDB()
        {
            await data.DeleteAllProducts();
            await data.InsertList(Products);
        }

        private async Task<bool> LoadProductsFromAPI()
        {
            var response = await apiService.GetList<Product>("https://fakestoreapi.com/products");
            if (!response.IsSuccess)
            {
                return false;
            }
            var list = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(list);
            return true;
        }
        private async void NavigationDetail()
        {
            IsRefresh = true;
            MainViewModel.MainViewModel.GetInstance().ProductsViewModel = new ProductsViewModel(SelectedProduct);
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProductDetail());
            IsRefresh = false;
        }
    }
}
