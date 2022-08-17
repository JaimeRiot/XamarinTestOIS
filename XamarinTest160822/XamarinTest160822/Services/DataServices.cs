using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinTest160822.Interface;
using XamarinTest160822.Model;

namespace XamarinTest160822.Services
{
    public class DataServices
    {
        private SQLiteAsyncConnection connection;
        public DataServices()
        {
            this.OpenOrCreateDB();
        }

        private async void OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDataBasePath();
            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<Product>().ConfigureAwait(false);
            await connection.CreateTableAsync<User>().ConfigureAwait(false);
        }
        public async Task Insert<T>(T model)
        {
            await connection.InsertAsync(model);
        }
        public async Task InsertList(ObservableCollection<Product> models)
        {
            await connection.InsertAllAsync(models);
        }
        public async Task<bool> FindUser(string email, string password)
        {
            var list = await GetAllUsers();
            var result = list.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var query = await connection.QueryAsync<User>("select * from [User]");
            var array = query.ToArray();
            var list = array.Select(p => new User
            {
                Email = p.Email,
                LastName = p.LastName,
                Name = p.Name,
                Password = p.Password
            }).ToList();

            return list;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var query = await connection.QueryAsync<Product>("select * from [Product]");
            var array = query.ToArray();
            var list = array.Select(p => new Product
            {
                Description = p.Description,
                Category = p.Category,
                Id = p.Id,
                Price = p.Price,
                Image = p.Image,
                Title = p.Title
            }).ToList();

            return list;
        }
        public async Task DeleteAllProducts()
        {
            var query = await this.connection.QueryAsync<Product>("delete from [Product]");
        }
    }
}
