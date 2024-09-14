using BriskApiSwym.Models;
using ClassLibrary1.Business;
using ClassLibrary1.Data.Framework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BriskApiSwym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordChangeController : ControllerBase
    {
        [HttpPost]
        public ActionResult passwordChange([FromBody] PasswordChangeModel pm)
        {


            InsertResult result = Accounts.changePassword(pm.email, pm.password);

            if (result.Succeeded)
            {
                return Ok(JsonConvert.SerializeObject("password changed succesfully"));
            }
            else
            {
                return BadRequest();
            }


        }
    }
}
