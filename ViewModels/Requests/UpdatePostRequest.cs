namespace ViewModels.Requests
{
    public class UpdatePostRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
