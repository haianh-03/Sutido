namespace Sutido.API.ViewModels.Responses
{
    public class UserResponse
    {
        public long UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? District { get; set; }
    }
}
