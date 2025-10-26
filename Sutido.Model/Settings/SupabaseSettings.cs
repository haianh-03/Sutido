namespace Sutido.Model.Settings
{
    public class SupabaseSettings
    {
        public string Url { get; set; } = null!;
        public string AnonKey { get; set; } = null!;
        public string ServiceRoleKey { get; set; } = null!;
        public string PublicBucket { get; set; } = null!;
        public string PrivateBucket { get; set; } = null!;
    }
}
