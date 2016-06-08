﻿using System;
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
        // =========== 
        [Route("api/tasksub/list/{t}")]
        public List<Models.MstTaskSub> Get(int t)
        {
            
            var tasksub = from d in db.trnTaskSubs
                          where d.TaskId == t
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

        // ===========
        // ADD Item
        // ===========
        [Route("api/tasksub/add")]
        public HttpResponseMessage Post(Models.MstTaskSub item)
        {
            try
            {
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.trnTaskSub newItem = new Data.trnTaskSub();

                newItem.TaskId = item.TaskId;
                newItem.Action = item.Action != null ? item.Action : "00000";

                if (item.DateCalled == null && item.TimeCalled == null)
                {
                    newItem.DateCalled = null;
                    newItem.TimeCalled = null;
                }
                else
                {
                    newItem.DateCalled = Convert.ToDateTime(item.DateCalled);
                    newItem.TimeCalled = Convert.ToDateTime(item.TimeCalled);
                }

                if (item.FinishedDate == null && item.FinishedTime == null)
                {
                    newItem.FinishedDate = null;
                    newItem.FinishedTime = null;
                }
                else
                {
                    newItem.FinishedDate = Convert.ToDateTime(item.FinishedDate);
                    newItem.FinishedTime = Convert.ToDateTime(item.FinishedTime);
                }
                newItem.Remarks = item.Remarks != null ? item.Remarks : "00000";

                //ALLOW NULL

                db.trnTaskSubs.InsertOnSubmit(newItem);
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
