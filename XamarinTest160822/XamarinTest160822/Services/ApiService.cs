using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinTest160822.Model;
using XamarinTest160822.Model.Response;

namespace XamarinTest160822.Services
{
    public class ApiService
    {
        HttpClient client;
        public ResponseClass CheckConnection()
        {
            var connection = Connectivity.NetworkAccess;
            if(connection != NetworkAccess.Internet)
            {
                return new ResponseClass
                {
                    IsSuccess = false,
                    Message = "No hay conexion a internet"
                };
            }
            return new ResponseClass
            {
                IsSuccess = true,
            };
        }
        public async Task<ResponseClass> GetList<T>(string urlBase)
        {
            try
            {
                client = new HttpClient();
                var response = await client.GetAsync(urlBase).ConfigureAwait(false);
                var answer = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return new ResponseClass
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new ResponseClass
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new ResponseClass()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
