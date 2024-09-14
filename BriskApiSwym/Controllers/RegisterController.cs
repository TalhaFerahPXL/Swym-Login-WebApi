using BriskApiSwym.Models;
using ClassLibrary1.Business;
using ClassLibrary1.Data.Framework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace BriskApiSwym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public ActionResult MakeNewUserFromBody([FromBody] RegisterModel vm)
        {
            SelectResult result = Accounts.CheckIfEmailExist(vm.email);


            if (result.Succeeded)
            {
                //return "Mail Already exist

                string text = "Invalid Email";

                return BadRequest(JsonConvert.SerializeObject(text));
            }
            else
            {
                string text = "Created";
                Accounts.AddAccounts(vm.email, vm.name, vm.password);

                return Ok(JsonConvert.SerializeObject(text));
            }
        }
    }
}
