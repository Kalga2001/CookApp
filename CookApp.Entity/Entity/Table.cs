using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.Entity.Entity
{
    public class Table : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; } 
        public bool IsAvailable { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
