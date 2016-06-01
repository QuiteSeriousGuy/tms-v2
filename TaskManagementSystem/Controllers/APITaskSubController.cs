using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystem.Controllers
{
    public class APITaskSubController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // LIST Item
        // =========== a
        [Route("api/tasksub/list")]
        public List<Models.MstTaskSub> Get()
        {
            
            var tasksub = from d in db.trnTaskSubs
                          select new Models.MstTaskSub
                          {
                              Id = d.Id,
                              TaskId = d.TaskId,
                              DateCalled = d.DateCalled.ToString(),
                              Action = d.Action,
                              TimeCalled = d.TimeCalled.ToString(),
                              FinishedDate = d.FinishedDate.ToString(),
                              FinishedTime = d.FinishedTime.ToString(),
                              Remarks = d.Remarks
                          };

            return tasksub.ToList();
        }
    }
}
