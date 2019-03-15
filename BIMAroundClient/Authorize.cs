using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Login;
using RestSharp;

namespace BIMAroundClient
{
    public class Authorize : IAuthorize
    {
        /// <summary>
        /// Base authentication method. If you want to use a different client address, you need to pass the clientUrl
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="clientUrl"></param>
        /// <returns></returns>
        public string GetToken(string login, string password, string clientUrl)
        {
            var loginRequest = new LoginRequest
            {
                login = login,
                password = password
            };

            var client = new RestClient(clientUrl);
            var request = new RestRequest("/login") {Method = Method.POST};
            request.AddJsonBody(loginRequest);


            var restResponse = client.Execute<LoginResponse>(request);

            var response = restResponse.Data;

            return response.token;
        }
    }
}
