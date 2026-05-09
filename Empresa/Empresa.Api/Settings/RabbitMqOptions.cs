namespace Empresa.Api.Settings
{
    public class RabbitMqOptions
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string VirtualHost { get; set; } = "/";
        public string Exchange { get; set; } = string.Empty;
        public int ManagementPort { get; set; } = 15672;
    }
}