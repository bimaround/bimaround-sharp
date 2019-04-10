using System;
using System.Net;
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
            string token = null;

            try
            {
                var loginRequest = new LoginRequest
                {
                    login = login,
                    password = password
                };

                var client = new RestClient(clientUrl);
                var request = new RestRequest("/login") { Method = Method.POST };
                request.AddJsonBody(loginRequest);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                       (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var restResponse = client.Execute<LoginResponse>(request);

                var response = restResponse.Data;

                token = response.token;
                //TODO change to serilog Logger.Error(restResponse.ErrorException);
            }
            catch (Exception e)
            {
                //TODO change to serilog Logger.Error(e);
            }

            return token;
        }
    }
}
