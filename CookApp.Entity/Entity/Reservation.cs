﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.Entity.Entity
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int NumberOfPeople { get; set; }
        public string Message { get; set; }
    }

}
