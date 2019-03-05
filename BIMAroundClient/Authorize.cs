using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel;
using RestSharp;

namespace BIMAroundClient
{
    public class Authorize : IAuthorize
    {
        public string GetToken(string login, string password)
        {
            var loginRequest = new LoginRequest
            {
                login = login,
                password = password
            };

            var client = new RestClient("https://bimaround.com/api");
            var request = new RestRequest("/login") {Method = Method.POST};
            request.AddJsonBody(loginRequest);


            var restResponse = client.Execute<LoginResponse>(request);

            LoginResponse response = restResponse.Data;

            return response.token;
        }
    }
}
