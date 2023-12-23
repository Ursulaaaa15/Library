using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Library.WebAPI.Settings
{
    public class LibrarySettings
    {
        public string LibraryDbContextConnectionString { get; set; }

        public Uri LibraryUri { get; set; }
        public string IdentityServerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret
        {
            get; set;
        }
}