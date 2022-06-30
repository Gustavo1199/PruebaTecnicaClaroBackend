using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PruebasTecnicaClaroDomBackend.Helper;
using PruebasTecnicaClaroDomBackend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PruebasTecnicaClaroDomBackend.Services
{
    public class BaseService
    {
       public string BaseUrl = "https://fakerestapi.azurewebsites.net/api/v1/";
        ResultResponses rr = new ResultResponses();
     
        public ResultResponses getallBooks()
        {
            //var BaseUrl = Configuration["BaseUrl"];
            try
            {
                var responde = new List<BooksModel>();

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(BaseUrl);

                var responses = client.GetAsync("Books").Result.Content.ReadAsStringAsync();

                responde = JsonConvert.DeserializeObject<List<BooksModel>>(responses.Result);
                 rr.GetSuccessOperation(responde);
            }
            catch (Exception ex)
            {
                rr.GetErrorOperation(ex);
            }
            return rr;
        }

        public ResultResponses AddBooks(BooksModel books)
        {
            //var BaseUrl = Configuration["BaseUrl"];
            try
            { 
           
            var responde = new BooksModel();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var responses = client.PostAsJsonAsync("Books",books).Result.Content.ReadAsStringAsync();

            responde = JsonConvert.DeserializeObject<BooksModel>(responses.Result);
            
            rr.GetSuccessOperation(responde);

            }
            catch (Exception ex)
            {

                rr.GetErrorOperation(ex);
            }
            return rr;
        }

        public ResultResponses GetBooksById(int id)
        {
            //var BaseUrl = Configuration["BaseUrl"];
            try
            {
            
            var responde = new BooksModel();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var responses = client.GetAsync($"Books/{id}").Result.Content.ReadAsStringAsync();

            responde = JsonConvert.DeserializeObject<BooksModel>(responses.Result);
            
            rr.GetSuccessOperation(responde);
            }
            catch (Exception ex)
            {
                rr.GetErrorOperation(ex);
            }
            return rr;
        }

        public ResultResponses UpdateBooks(BooksModel books, int id)
        {
            try
            {
                var responde = new BooksModel();

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(BaseUrl);

                var responses = client.PutAsJsonAsync($"Books/{id}", books).Result.Content.ReadAsStringAsync();

                responde = JsonConvert.DeserializeObject<BooksModel>(responses.Result);
              rr.GetSuccessOperation(responde);
            }
            catch (Exception ex)
            {
                rr.GetErrorOperation(ex);
            }

            return rr;
        }

        public ResultResponses DeleteBooks(int id)
        {
            try
            {
                var responde = new BooksModel();

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(BaseUrl);

                var responses = client.DeleteAsync($"Books/{id}").Result.Content.ReadAsStringAsync();

              
                rr.GetSuccessOperation("Eliminado Correctamente");
            }
            catch (Exception ex)
            {

                rr.GetErrorOperation(ex);
            }
            return rr;
        }


    }
}
