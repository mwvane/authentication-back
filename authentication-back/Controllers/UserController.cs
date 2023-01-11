using Microsoft.AspNetCore.Mvc;

namespace authentication_back.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            users = StorageHelper.GetUsers();
            if(users == null)
            {
                users= new List<User>();
            }
        }
        private readonly List<User> users = new List<User>();
        [HttpPost("login")]
        public User Login([FromBody] Dictionary<string, string> payload)
        {
            if(payload.ContainsKey("username") && payload.ContainsKey("password"))
            {
                string userneme = payload["username"];
                string password = payload["password"];
                if(users != null)
                {
                    foreach (var user in users)
                    {
                        if (user.Email == userneme && user.Password == password)
                        {
                            return user;
                        }
                    }
                }
            }
            return null;
        }
        [HttpPost("register")]
        public bool Register([FromBody] Dictionary<string, string> user) {

            User tmp = new User()
            {
                Id = 1,
                Email= user["username"],
                Firstname= user["firstname"],
                Lastname = user["lastname"],
                Password = user["password"],
            };
            this.users.Add(tmp);
            try
            {
                StorageHelper.Save(users);
                return true;
            }
            catch(Exception ex){ return false; }
        }
    }
}
