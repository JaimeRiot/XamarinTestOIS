using System;
using System.Collections.Generic;
using System.Text;
using XamarinTest160822.ViewModel.MainViewModel;

namespace XamarinTest160822.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
