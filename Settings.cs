using Microsoft.Extensions.Configuration;

namespace DataIngestion.TestAssignment
{
    public class Settings
    {
        public string ElasticSearchUrl { get; set; }

        public Settings()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            ElasticSearchUrl = config["ElasticSearchUrl"];
        }
    }
}
