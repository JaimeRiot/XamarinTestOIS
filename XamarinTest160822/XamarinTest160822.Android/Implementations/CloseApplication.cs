using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinTest160822.Interface;
[assembly:Dependency(typeof(XamarinTest160822.Droid.Implementations.CloseApplication))]
namespace XamarinTest160822.Droid.Implementations
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}