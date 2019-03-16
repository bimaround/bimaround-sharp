using BIMAroundClient;
using BIMAroundClient.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIMAroundClientTests
{
    [TestClass()]
    public class AuthorizeTests
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        private static string Login => "localUser";
        private static string Password => "Qwerty123";
        private static string ClientUrl => "http://localhost:8080/api";

        private readonly IAuthorize _authorize = new Authorize();

        [TestMethod()]
        public void GetTokenTest()
        {
            var token = _authorize.GetToken(Login, Password, ClientUrl);

            if (string.IsNullOrEmpty(token))
            {
                Assert.Fail("token not received");
            }
            else
            {
                TestContext.WriteLine(token);
            }
        }
    }
}