using CookApp.BLL.Dtos.ReservationDtos;
using CookApp.BLL.Services;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IReservationService
    {
        Task<bool> MakeReservation(ReservationDto reservationDto);
        Task<List<string>> GetAvailableTimes(DateTime reservationDate, int? tableId);
        Task<List<int>> GetAvailableTables(DateTime? reservationDate, string time);
        Task<List<int>> GetTables();
        List<string> GetTime();
    }

}
