﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class Statuses
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
