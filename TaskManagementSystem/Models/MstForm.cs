using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class MstForm
    {
        [Key]
        public Int32 Id { get; set; }
        public String Form { get; set; }
        public String FormDescription { get; set; }
    }
}