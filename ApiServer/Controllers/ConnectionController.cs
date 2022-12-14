using System;
using Microsoft.AspNetCore.Mvc;
using ApiServer;
using Npgsql;
namespace ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ConnectionController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetConnection")]
        public Response GetConnection()
        {
            return new Response { Error = true, Data = new Connection() };
        }

        [HttpPost("Connection")]
        public async Task<Response> Connection([FromBody] Connection connection)
        {
            if (connection == null || string.IsNullOrEmpty(connection.Name) || string.IsNullOrEmpty(connection.Password))
            {
                return new Response { Error = true, Data = new ArgumentException("an argument is missing"), Code = 412 };
            }
            else
            {
                try
                {
                    var connectionstring = _config.GetValue<string>("ConnectionStrings:Database");
                    using var conn = new NpgsqlConnection(connectionstring);
                    using var command = new NpgsqlCommand();
                    command.Connection = conn;
                    await conn.OpenAsync();
                    command.CommandText = @"Select ""Id"" From ""public"".""User"" Where ""Name""= @Name And ""Password"" = @Password ";
                    command.Parameters.Add(new NpgsqlParameter("@Name", connection.Name));
                    command.Parameters.Add(new NpgsqlParameter("@Password", connection.Password));
                    var id = await command.ExecuteScalarAsync();
                    if(id == null || string.IsNullOrEmpty(id.ToString()))
                    {
                        await conn.CloseAsync();
                        return new Response { Error = true, Data = "Your account doesn't exist", Code = 404 };
                    }
                    await conn.CloseAsync();
                    return new Response { Error = false, Data = "You are connected", Code = 200 };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new Response { Error = true, Data = "Unknown Error", Code = 500};
                }
            }
        }
    }
}