using Microsoft.AspNetCore.Mvc;

namespace authentication_back.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            users = StorageHelper.GetUsers() ?? new List<User>();
        }

        private readonly List<User> users = new();

        [HttpPost("login")]
        public Result Login([FromBody] Dictionary<string, string> payload)
        {
            if (payload.ContainsKey("username") && payload.ContainsKey("password"))
            {
                string userneme = payload["username"];
                string password = payload["password"];

                foreach (var user in users)
                {
                    if (user.Email == userneme && user.Password == password)
                    {
                        return new Result() { Res = user};
                    }
                }
            }
            return new Result() { Res = false, Error = "Such user not found!"};
        }

        [HttpPost("register")]
        public Result Register([FromBody] Dictionary<string, string> user)
        {
            if (!Helpers.isUsernameAlreadyExist(user["email"], users)){
                User newUser = new()
                {
                    Id = Helpers.GetMaxId(users) + 1,
                    Email = user["email"],
                    Firstname = user["firstname"],
                    Lastname = user["lastname"],
                    Password = user["password"],
                };

                users.Add(newUser);

                try
                {
                    StorageHelper.Save(users);
                    return new Result() { Res = true };
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    return new Result() { Res = null, Error = exp.Message};
                }
            }
            return new Result() { Res = null, Error = "User already exist!"};
        }
    }
}
