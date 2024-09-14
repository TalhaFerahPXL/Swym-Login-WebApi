using BriskApiSwym.Models;
using ClassLibrary1.Business;
using ClassLibrary1.Business.Entities;
using ClassLibrary1.Data.Framework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BriskApiSwym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]

        public ActionResult CheckLoginCredentials([FromBody] LoginModel user)
        {
            SelectResult result = Accounts.CheckLoginCredentials(user.Email, user.Password);

            if (result.Rows == 1)
            {
                string hashedPasswordFromDatabase = result.DataTable.Rows[0]["password"].ToString();

                // Controleer of het ingevoerde wachtwoord overeenkomt met het gehashte wachtwoord in de database
                bool passwordMatch = BCrypt.Net.BCrypt.Verify(user.Password, hashedPasswordFromDatabase);

                if (passwordMatch)
                {
                    SelectResult nameResult = Accounts.GetNameFromEmail(user.Email);
                    string name = nameResult.DataTable.Rows[0]["name"].ToString();

                    return Ok(JsonConvert.SerializeObject($"Welcome back {name}"));
                }
                else
                {
                    return BadRequest("Invalid password");
                }
            }
            else
            {
                return BadRequest("Invalid login information.");
            }
        }


    }
}
