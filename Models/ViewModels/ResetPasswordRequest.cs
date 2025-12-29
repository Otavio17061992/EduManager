namespace EduManager.Models.ViewModels
{
    public class ResetPasswordRequest
    {
        public string? Cpf { get; set; }
        public string? NewPassword { get; set; }
    }
}