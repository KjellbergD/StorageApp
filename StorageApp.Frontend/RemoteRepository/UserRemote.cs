using System.Net.Http;
using System.Threading.Tasks;
using StorageApp.Shared;
using System.Net;
using System.Text.Json;
using System;
using System.Text;
using System.Net.Http.Json;

namespace StorageApp.Frontend
{
    public class UserRemote : IUserRemote
    {
        private readonly HttpClient _httpClient;

        public UserRemote(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //TODO: Find better return type.
        public async Task<HttpStatusCode> CreateUser(UserCreateDTO user)
        {
            var result = await _httpClient.PostAsJsonAsync("User", user); 
            return result.StatusCode;
        }

        public async Task<HttpStatusCode> LoginUser(UserLoginDTO user)
        {
            var result = await _httpClient.PostAsJsonAsync("User/login", user);
            return result.StatusCode;
        }
    }
}