using authentication_back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace authentication_back.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        public UserController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("getUser")]
        public Result Getuser(string username)
        {
            User user = _context.Users.Where(user => user.Email == username).FirstOrDefault();
            if (user == null)
            {
                return new Result() { Errors = new List<string>() {"Such user not found!"} };
            }
            return new Result() { Res = user.Id };
        }

        [HttpPost("updatePassword")]
        public Result UpdatePassword([FromBody] Dictionary<string, string> payload)
        {
            try
            {
                User user = _context.Users.Where(user => user.Id == int.Parse(payload["id"])).FirstOrDefault();
                if (user != null)
                {
                    user.Password = payload["password"];
                    user.PasswordLastUpdatedAt = DateTime.Now;
                    _context.SaveChanges();
                    return new Result() { Res = true };
                }
            }
            catch
            {
                return new Result() { Res = false };
            }
            return new Result() { Res = false };
        }

        [HttpPost("login")]
        public Result Login([FromBody] Dictionary<string, string> payload)
        {
            if (payload.ContainsKey("username") && payload.ContainsKey("password"))
            {
                string userneme = payload["username"];
                string password = payload["password"];
                User user = null;
                try
                {
                    user = _context.Users.Where(user => user.Email == userneme && user.Password == password).FirstOrDefault();
                    if (user != null)
                    {
                        return new Result() { Res = user };
                    }
                    return new Result() { Res = null, Errors = new List<string>() { "Incorrect username or passsword" } };
                }
                catch
                {
                    return new Result() { Res = null, Errors = new List<string>() { "something went wrong" } };
                }



            }
            return new Result() { Res = false, Errors = new List<string>() { "Username or passwprd is incorrect!" } };
        }

        [HttpPost("register")]
        public Result Register([FromBody] Dictionary<string, string> user)
        {
            bool usernameAlreadyExists = false;

            usernameAlreadyExists = _context.Users.Where(u => u.Email == user["email"]).FirstOrDefault() != null;

            if (!usernameAlreadyExists)
            {
                User newUser = new()
                {
                    Email = user["email"],
                    Firstname = user["firstname"],
                    Lastname = user["lastname"],
                    Password = user["password"],
                };
                List<string> errors = Validateuser(newUser);
                if (errors.Count == 0)
                {
                    _context.Add(newUser);
                    try
                    {
                        _context.SaveChanges();
                        return new Result() { Res = true };
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                        return new Result() { Res = null, Errors = errors };
                    }
                }
                return new Result() { Res = null, Errors = errors };
            }
            return new Result() { Res = null, Errors = new List<string>() { "User already exists!" } };
        }

        private List<string> Validateuser(User user)
        {
            List<string> errors = new List<string>();
            if (user.Firstname.Length < 2)
            {
                errors.Add("Firstname minimum length must be 2 character");
            }
            if (user.Password.Length < 6)
            {
                errors.Add("Password minimum length must be 6 symbols");
            }
            if (IsEmailValid(user.Email))
            {
                errors.Add("Incorrect email format");

            }
            return errors;
        }
        private bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, "^[a-zA-Z0-9_.+-]+@[email]+\\.[a-zA-Z0-9-.]+$", RegexOptions.IgnoreCase) == true;
        }
    }
}
