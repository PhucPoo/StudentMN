namespace StudentMN.DTOs.Response
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public UserInfo? User { get; set; }
        public DateTime ExpiredAt { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
