namespace ApiServer
{
    public class Response
    {
        public Boolean Error { get; set; }

        public object? Data { get; set; }

        public int Code { get; set; }

    }
}
