using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class TableService : ITableService
    {
        private readonly IGenericRepository<Table> _tableRepository;

        public TableService(IGenericRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime reservationDate, int numberOfPeople)
        {
            return await _tableRepository.GetAllAsyncQuery()
                .Where(t => t.Capacity >= numberOfPeople && t.IsAvailable)
                .Include(t => t.Reservations)
                .Where(t => t.Reservations.All(r => r.ReservationDate != reservationDate))
                .ToListAsync();
        }
    }

}
