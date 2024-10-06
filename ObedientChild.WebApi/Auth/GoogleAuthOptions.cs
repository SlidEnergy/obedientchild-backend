namespace ObedientChild.WebApi.Auth
{
    public class GoogleAuthOptions
    {
        public string RedirectUrl { get; set; }
        public string FrontendRedirectUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
