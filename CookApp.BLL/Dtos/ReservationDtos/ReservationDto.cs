using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Dtos.ReservationDtos
{
    public class ReservationDto
    {
        public string Name { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; } 
        public DateTime ReservationDate { get; set; }
        public TimeSpan Time { get; set; }
        public int NumberOfPeople { get; set; } 
        public string Message { get; set; }
    }
}
