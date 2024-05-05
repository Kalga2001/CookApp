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

    }
}
