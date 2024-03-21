namespace Domain.Configuration
{
    public class Secrets
    {
        public Secrets()
        {
            ClientSecret = string.Empty;
            PreSalt = string.Empty;
            PosSalt = string.Empty;
            ConnectionString = string.Empty;
            Rabbit_Hostname = string.Empty;
            Rabbit_Password = string.Empty;
            Rabbit_Username = string.Empty;
            Rabbit_Port = string.Empty;
            Rabbit_VirtualHost = string.Empty;
            ExchangeEspelhoPonto = string.Empty;
        }

        public string ClientSecret { get; set; }
        public string PreSalt { get; set; }
        public string PosSalt { get; set; }
        public string ConnectionString { get; set; }

        public string Rabbit_Hostname { get; set; }
        public string Rabbit_Port { get; set; }
        public string Rabbit_Username { get; set; }
        public string Rabbit_Password { get; set; }
        public string Rabbit_VirtualHost { get; set; }
        public string ExchangeEspelhoPonto { get; set; }
    }
}