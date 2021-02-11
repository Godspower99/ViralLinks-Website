namespace ViralLinks.Data
{
    public class AzureStorageConfigOptions
    {
        public const string Name = "AzureStorageConfig";
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string ProfilePicturesContainer { get; set; }
    }

    public class DatabaseConnectionOptions
    {
        public const string Name = "DatabaseConnections";
        public string Database { get; set; }
    }
}