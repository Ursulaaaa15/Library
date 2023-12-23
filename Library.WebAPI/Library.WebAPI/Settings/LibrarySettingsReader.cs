namespace Library.WebAPI.Settings
{
    public static class LibrarySettingsReader
    {
        public static LibrarySettings Read(IConfiguration configuration)
        {
            return new LibrarySettings()
            {
                LibraryUri = configuration.GetValue<Uri>("Uri"),
                LibraryDbContextConnectionString = configuration.GetValue<string>("ElectronicMenuDbContext"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            };
        }
    }
}