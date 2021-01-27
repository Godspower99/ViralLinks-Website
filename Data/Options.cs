namespace ViralLinks.Data
{
    public class AzureStorageConfigOptions
    {
        public const string Name = "AzureStorageConfig";
    }

    public class DatabaseConnectionOptions
    {
        public const string Name = "DatabaseConnections";
        public string Database { get; set; }
    }
}