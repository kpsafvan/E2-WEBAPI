using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

using System.Collections.Generic;
using WEBAPI_E2.Data;
using WEBAPI_E2.Models;

namespace WEBAPI_E2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public readonly IConfiguration _configuration;


        private readonly WEBAPI_E2DbContext _dbContext;

        public RegistrationController(WEBAPI_E2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        [HttpPost]
        [Route("Login")]
        public  ActionResult Login(login credentials)
        {
            bool login = false;
            var datas = new { UserId = "", isActive = false };

            var details = _dbContext.Registration.AsNoTracking().ToList();
            foreach(var item in details)
            {
                if (item.UserName.Equals(credentials.username) && item.Password.Equals(credentials.password))
                {
                    login = true;
                    return Ok(new { Name=item.UserName,UserId=item.ID,isActive = item.IsActive  }); 
                }
            }
            if(!login)
            { 
                return BadRequest("Login Failed ! Check your credentials");
            }
            else
            {
                return Ok();
            }
           /*
            if(credentials.username.Equals("asd")&& credentials.password.Equals("asd"))
            {
                return "passwords math";
            }
            return "done";
           */
        }

        [HttpPost]
        [Route("Register")]
        public string Register(RegistrationModel Entry)
        {
            //Need to write validations
            if (ModelState.IsValid)
            {
                var i = _dbContext.Registration.Add(Entry);
                _dbContext.SaveChanges();
                return "Succussfully addede user " + Entry.UserName;
            }
            else
                return "Please Enter a valid entry";

   
        }
        /*[HttpPost]
        [Route("registration")]
        public string registration(RegistrationModel registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LearnDB").ToString());
            SqlCommand cmd = new SqlCommand("Insert into registration(UserName,PassWord,Email,IsActive) Values('"+registration.UserName+"','"+registration.Password+"','"+registration.Email+"','"+registration.IsActive+"')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data Inserted";
            }
            else
            {
                return "Not inserted";
            }
        }*/
        public class login
        {
            public string  username { get; set; }
            public string password { get; set; }
        }
    }
}
