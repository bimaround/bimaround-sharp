namespace BIMAroundClient.ObjectModel.Login
{
    class LoginRequest
    {
        public string login { get; set; }//не менять на на заглавную, иначе
        public string password { get; set; }//в api не парсится json
    }
}
