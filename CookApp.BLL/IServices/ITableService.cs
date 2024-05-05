using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime reservationDate, int numberOfPeople);
    }
}
