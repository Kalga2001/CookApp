using CookApp.BLL.Dtos.ReservationDtos;
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
            var tableExists = await _tableRepository.GetByIdAsync(reservationDto.TableId) != null;
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
                EndTime = reservationDto.Time.Add(TimeSpan.FromHours(2)),
                NumberOfPeople = reservationDto.NumberOfPeople,
                Message = reservationDto.Message
            };

            var added = await _reservationRepository.AddAsync(reservation);
            return added != null;
        }

        public async Task<List<string>> GetAvailableTimes(DateTime reservationDate, int? tableId)
        {
            var allTimes = GetTime();

            var reservedTimes = await _reservationRepository.GetAllAsyncQuery()
                .Where(r => r.ReservationDate == reservationDate && (!tableId.HasValue || r.TableId == tableId))
                .Select(r => r.BeginTime.ToString(@"hh\:mm"))
                .ToListAsync();

            return allTimes.Except(reservedTimes).ToList();
        }

        public async Task<List<int>> GetAvailableTables(DateTime? reservationDate, string time)
        {
            var timeParts = time.Split(':').Select(int.Parse).ToArray();
            var timeSpan = new TimeSpan(timeParts[0], timeParts[1], 0);

            var reservedTables = await _reservationRepository.GetAllAsyncQuery()
                .Where(r => r.ReservationDate == reservationDate && r.BeginTime == timeSpan)
                .Select(r => r.TableId)
                .ToListAsync();

            var allTables = await _tableRepository.GetAllAsyncQuery()
                .Select(t => t.Id)
                .ToListAsync();

            return allTables.Except(reservedTables).ToList();
        }

        public async Task<List<int>> GetTables()
        {
            var tables = await _tableRepository.GetAllAsyncQuery().Select(x => x.Id).ToListAsync();
            return tables;
        }

        public List<string> GetTime()
        {
            var startTime = new TimeSpan(7, 0, 0); 
            var endTime = new TimeSpan(23, 0, 0);  
            var timeSlots = new List<string>();

            for (var time = startTime; time < endTime; time = time.Add(TimeSpan.FromHours(1)))
            {
                timeSlots.Add(time.ToString(@"hh\:mm"));
            }

            return timeSlots;
        }
    }

}
