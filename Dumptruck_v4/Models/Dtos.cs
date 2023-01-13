namespace Dumptruck_v4.Models {
    public class LoginDto {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoggedInDto {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}
