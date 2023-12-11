namespace ApiRestBilling5.Models.DTOs
{
    public class UserLoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string   Token { get; set; }
    }
}
