using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystem.Controllers
{
    public class APIUserController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // Get Item
        // =========== 

        [Route("api/user/search/{id}")]
        public List<Models.MstUser> GetItem(String id)
        {
            var isLocked = true;

            var ID = Convert.ToInt32(id);
            var user = from d in db.mstUsers
                       where d.StaffId == ID
                       select new Models.MstUser
                       {
                           StaffId = d.StaffId,
                           Username = d.Username,
                           Password = d.Password,
                           Designation = d.Designation,
                           IsLocked = isLocked
                       };

            return user.ToList();
        }

        // ===========
        // LIST Item
        // =========== 
        [Route("api/user/list")]
        public List<Models.MstUser> Get()
        {
            var isLocked = true;

            var user = from d in db.mstUsers
                          select new Models.MstUser
                          {
                              StaffId = d.StaffId,
                              Username = d.Username,
                              IsLocked = isLocked
                          };

            return user.ToList();
        }


    }
}
