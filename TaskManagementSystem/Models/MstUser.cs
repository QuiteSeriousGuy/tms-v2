﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class MstUser
    {
        [Key]
        public Int32 StaffId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public Int32? Designation { get; set; }
        public Boolean IsLocked { get; set; }
    }
}