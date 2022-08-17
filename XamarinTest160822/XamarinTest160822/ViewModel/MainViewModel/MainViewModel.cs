using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinTest160822.ViewModel.MainViewModel
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public ProductsViewModel.ProductsViewModel ProductsViewModel { get; set; }
        public UserViewModel.UserViewModel UserViewModel { get; set; }
        public MainViewModel()
        {
            instance = this;
        }
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
