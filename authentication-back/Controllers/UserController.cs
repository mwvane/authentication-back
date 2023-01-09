using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace authentication_back.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly User user = new User()
        {
            Id=1,
            Email = "bzishvili57@gmail.com",
            Firstname = "Levan",
            Lastname = "bzisvhili",
            Password = "test"
        };
        [HttpGet("test")]
        public User LeoGet()
        {
            return user;
        }

        //[Route("api/[controller]/login")]
        [HttpPost("login")]
        public User Login([FromBody] Dictionary<string, string> payload)
        {
            if(payload.ContainsKey("username") && payload.ContainsKey("password"))
            {
                string us = payload["username"];
                string pas = payload["password"];
                if (payload["username"] == user.Email && payload["password"] == user.Password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
