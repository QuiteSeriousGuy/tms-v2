using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class MstTaskSub
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 TaskId { get; set; }
        public String DateCalled { get; set; }
        public String Action { get; set; }
        public String TimeCalled { get; set; }
        public String FinishedDate { get; set; }
        public String FinishedTime { get; set; }
        public String Remarks { get; set; }
    }
}