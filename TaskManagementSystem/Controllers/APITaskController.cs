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

        // ===========
        // ADD Item
        // ===========
        [Route("api/task/add")]
        public HttpResponseMessage Post(Models.MstTask item)
        {
            try
            {
                var isLocked = true;
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.trnTask newItem = new Data.trnTask();

                newItem.TaskNo = item.TaskNo != null ? item.TaskNo : "00000";
                newItem.TaskDate = Convert.ToDateTime(item.TaskDate);
                newItem.ClientId = item.ClientId;
                newItem.Caller = item.Caller != null ? item.Caller : "00000";
                newItem.Concern = item.Caller != null ? item.Caller : "00000";
                newItem.AnsweredBy = item.AnsweredBy;
                newItem.StaffId = item.StaffId;
                newItem.ProductId = item.ProductId;
                newItem.Remarks = item.Remarks != null ? item.Remarks: "00000"; 
                newItem.Status = item.Status != null ? item.Status : "00000";
                newItem.ProblemType = item.ProblemType != null ? item.ProblemType : "00000";
                newItem.Severity = item.Severity != null ? item.Severity : "00000";
                newItem.Solution = item.Solution != null ? item.Solution : "00000";
                if (item.DoneDate == null && item.DoneTime == null)
                {
                    newItem.DoneDate = null;
                    newItem.DoneTime = null;
                }
                else
                {
                    newItem.DoneDate = Convert.ToDateTime(item.DoneDate);
                    newItem.DoneTime = Convert.ToDateTime(item.DoneTime);
                }
                newItem.VerifiedBy = item.VerifiedBy;
                newItem.IsLocked = isLocked;

                //ALLOW NULL

                db.trnTasks.InsertOnSubmit(newItem);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
