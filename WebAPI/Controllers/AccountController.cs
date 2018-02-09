using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/profile")]
    public class AccountController : ApiController
    {
        private List<LoginModel> _logList = null;

        public AccountController()
        {
            _logList = new List<LoginModel>();
            _logList.Add(new LoginModel { Username = "shambhu1", Password = "shambhu1" });
            _logList.Add(new LoginModel { Username = "shambhu2", Password = "shambhu2" });
            _logList.Add(new LoginModel { Username = "shambhu3", Password = "shambhu3" });
        }
        /// <summary>
        /// Extract list of all registered users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(Name = "getUsers")]
        [ActionName("getUsers")]
        [ResponseType(typeof(List<tblAccount>))]
        public IHttpActionResult getAllUser()
        {
            List<tblAccount> model = new List<tblAccount>();
            //return new string[] { "value1", "value2" };
            return Ok(model);
        }

        /// <summary>
        /// Extract individual user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(Name = "getUser/{id:long}")]
        [ResponseType(typeof(tblAccount))]
        public IHttpActionResult getUserById(long id)
        {
            tblAccount model = new tblAccount();
            return Ok(model);
        }

        /// <summary>
        /// Update profile information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Name = "updateProfiles")]
        [ResponseType(typeof(tblAccount))]
        public IHttpActionResult Profile(tblAccount model)
        {
            return Ok(model);
        }

        /// <summary>
        /// Login using Username and Password
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route(Name = "login")]
        public IHttpActionResult Login(string Username, string Password)
        {
            bool returnStatus = false;

            return Ok(returnStatus);
        }

        [HttpPatch]
        [Route(Name = "update")]
        public IHttpActionResult Update(LoginModel model)
        {
            string responseMessage = "Duplicate Request";
            if (PreventSpam.IsDuplicateRequest(model.Username, model.Password))
            {
                var result = (from sel in _logList where sel.Username.Equals(model.Username) select sel).FirstOrDefault();
                result.Username = model.Username;
                result.Password = model.Password;
                responseMessage = "Proper request";
            }
            return Ok(responseMessage);
        }
    }
}
