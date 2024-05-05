using CookApp.BLL.Dtos.ReservationDtos;
using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;
        private readonly IGenericRepository<Table> _tableRepository;

        public ReservationService(IGenericRepository<Reservation> reservationRepository, IGenericRepository<Table> tableRepository)
        {
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
        }
 
        public async Task<bool> MakeReservation(ReservationDto reservationDto)
        {
            // Проверяем существование стола с указанным TableId
            var tableExists = await _tableRepository.GetByIdAsync(reservationDto.TableId) != null ? true : false;
            if (!tableExists)
            {
                throw new ArgumentException("Table with the provided TableId does not exist.");
            }

            var reservation = new Reservation
            {
                TableId = reservationDto.TableId,
                UserId = reservationDto.UserId,
                ReservationDate = reservationDto.ReservationDate,
                BeginTime = reservationDto.Time,
                EndTime = reservationDto.Time.AddHours(2),
                NumberOfPeople = reservationDto.NumberOfPeople,
                Message = reservationDto.Message
            };

            return await _reservationRepository.AddAsync(reservation);
        }


    }

}
