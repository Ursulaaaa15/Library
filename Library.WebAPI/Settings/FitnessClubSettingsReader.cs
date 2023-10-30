namespace Library.WebAPI.Settings
{
    public static class FitnessClubSettingsReader
    {
        public static FitnessClubSettings Read(IConfiguration configuration)
        {
          
            return new FitnessClubSettings();
        }
    }
}