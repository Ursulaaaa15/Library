namespace Library.WebAPI.Settings
{
    public static class LibrarySettingsReader
    {
        public static LibrarySettings Read(IConfiguration configuration)
        {
          
            return new LibrarySettings()
            {
                LibraryDbContextConnectionString = configuration.GetValue<string>("LibraryDbContext")
            };
        }
    }
}