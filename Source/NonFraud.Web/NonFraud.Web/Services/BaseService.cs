using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NonFraud.Web.Helpers;
using NonFraud.Web.Models;

namespace NonFraud.Web.Services
{
    /// <summary>
    /// Class for consume dynamically the WebAPI methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> where T : class
    {
        readonly string baseUri = ContextValues.ApiUrl;

        /// <summary>
        /// Insert dynamic method
        /// </summary>
        /// <param name="model">Model to send to the service</param>
        /// <param name="path">Service Method path</param>
        /// <returns></returns>
        public ResponseModel Create(T model, string path)
        {
            ResponseModel responseModel = new ResponseModel();
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(baseUri + path, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Result = String.Empty;
                    responseModel.IsValid = true;
                }
                else
                {
                    responseModel.Result = response.ReasonPhrase;
                    responseModel.IsValid = false;
                }
            }

            return responseModel;
        }

        public string Get(string path)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(baseUri);
                HttpResponseMessage response = client.GetAsync(baseUri + path).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        public ResponseModel Update(T model, string path)
        {
            ResponseModel responseModel = new ResponseModel();
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync(baseUri + path, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Result = String.Empty;
                    responseModel.IsValid = true;
                }
                else
                {
                    responseModel.Result = response.ReasonPhrase;
                    responseModel.IsValid = false;
                }
            }

            return responseModel;
        }

        public ResponseModel Delete(int id, string path)
        {
            ResponseModel responseModel = new ResponseModel();
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync(baseUri + path + "/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Result = String.Empty;
                    responseModel.IsValid = true;
                }
                else
                {
                    responseModel.Result = response.ReasonPhrase;
                    responseModel.IsValid = false;
                }
            }

            return responseModel;
        }
    }
}