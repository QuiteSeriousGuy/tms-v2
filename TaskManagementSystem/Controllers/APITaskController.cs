using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystem.Controllers
{
    public class APITaskController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // LIST Item
        // =========== 
        [HttpGet]
        [Route("api/task/listByDate/{date}")] //
        public List<Models.MstTask> get(String date) //
        {
            var task = from d in db.trnTasks
                       where d.TaskDate == Convert.ToDateTime(date)
                       select new Models.MstTask
                       {
                           Id = d.Id,
                           TaskNo = d.TaskNo,
                           TaskDate = d.TaskDate.ToShortDateString(),
                           ClientId = d.ClientId,
                           Caller = d.Caller,
                           Concern = d.Concern,
                           AnsweredBy = d.AnsweredBy,
                           StaffId = d.StaffId,
                           ProductId = d.ProductId,
                           ProductCode = d.mstProduct.ProductCode,
                           Remarks = d.Remarks,
                           Status = d.Status,
                           ProblemType = d.ProblemType,
                           Severity = d.Severity,
                           Solution = d.Solution,
                           DoneDate = d.DoneDate.ToString(),
                           DoneTime = d.DoneTime.ToString(),
                           VerifiedBy = d.VerifiedBy,
                           IsLocked = d.IsLocked
                       };

            return task.ToList();

        }

        // ===========
        // LIST Item order by last
        // =========== 
        [HttpGet]
        [Route("api/task/listByOrder/{date}")] //
        public Models.MstTask getDate(String date) //
        {
            var task = from d in db.trnTasks.OrderByDescending(d => d.Id)
                       where d.TaskDate == Convert.ToDateTime(date)
                       select new Models.MstTask
                       {
                           TaskNo = d.TaskNo,
                       };

            return (Models.MstTask)task.FirstOrDefault();

        }

    }
}
