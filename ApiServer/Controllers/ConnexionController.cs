using System;
using Microsoft.AspNetCore.Mvc;
using ApiServer;
namespace ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnexionController : ControllerBase
    {
        public ConnexionController()
        {
        }

        [HttpGet("GetConnexion")]
        public Response GetConnexion()
        {
            return new Response { Error = true, Data = new Connexion() };
        }

        [HttpPost("Connexion")]
        public Response Connexion([FromBody] Connexion connexion)
        {
            if (connexion == null || string.IsNullOrEmpty(connexion.Name) || string.IsNullOrEmpty(connexion.Password) || string.IsNullOrEmpty(connexion.Ip))
            {
                return new Response { Error = true, Data = new ArgumentException("an argument is missing") };
            }
            else
            {
                return new Response { Error = false, Data = "Your are connected" };
            }
        }
    }
}