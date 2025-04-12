    namespace Bridge_server.DTOs.Auth
    {
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
