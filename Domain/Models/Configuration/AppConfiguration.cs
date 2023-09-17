namespace Domain.Models.Configuration
{
    public class AppConfiguration
    {

        public string? ConnectionString { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set;}
        public AppConfiguration() { }
        public AppConfiguration(string connectionString, string admin, string adminPassword)
        {
            ConnectionString = connectionString;
            Username = admin;
            Password = adminPassword;
        }
    }
}
