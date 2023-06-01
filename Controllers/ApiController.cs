using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<Users> GetUsers()
        {
            try
            {
                Object obj;
                List<Users> lstUsers = new List<Users>();
                Users users = new Users();
                string cstring = "Server=172.21.130.220;Port=3306;User ID=ambiuser;Password=ambi@12345;Database=AmbiDB";
                using var connection = new MySqlConnection(cstring);
                connection.Open();
                using var command = new MySqlCommand("SELECT Top 1 * FROM Users;", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    obj = reader.GetValue(0);
                    users = (Users)obj;
                }
                lstUsers.Add(users);
                return lstUsers;
            }
            catch
            {
                List<Users> lstUsers = new List<Users>();
                Users users = new Users();
                users.UserID = 1;
                users.Mobile = "7501197669";
                users.IsServiceProvider = false;
                users.Longitude = "88.370210";
                users.Lantitude = "22.565570";
                users.RegisterDated = DateTime.Now;
                lstUsers.Add(users);
                return lstUsers;
            }
        }
    }
}
