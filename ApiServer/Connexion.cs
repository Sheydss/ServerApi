namespace ApiServer
{
    public class Connexion
    {
        public string? Name { get; set; }
        public string? Password{ get; set; }
        public Boolean New { get; set; } = false;
        public string? Ip { get; set; }
    }
}