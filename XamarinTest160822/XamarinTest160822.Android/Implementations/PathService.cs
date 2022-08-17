
using System;
using System.IO;
using XamarinTest160822.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinTest160822.Droid.Implementations.PathService))]
namespace XamarinTest160822.Droid.Implementations
{
    internal class PathService: IPathService
    {
        public string GetDataBasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "localdatabase.db3");
        }
    }
}