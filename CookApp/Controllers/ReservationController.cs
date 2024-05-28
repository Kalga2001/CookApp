using CookApp.BLL.Dtos.ReservationDtos;
using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CookApp.API.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IOrderService _orderService;
        public ReservationController(IReservationService reservationService, IOrderService orderService)
        {
            _reservationService = reservationService;
            _orderService = orderService;
        }

        [HttpGet]
        [CustomAuthorize("Administrator", "Client")]
        public async Task<IActionResult> Index()
        {
            DateTime currentDate = DateTime.Today;

            var availableTables = await _reservationService.GetTables();
            var availableTime = _reservationService.GetTime();

            ViewBag.AvailableTables = availableTables;
            ViewBag.AvailableTime = availableTime;

            return View();
        }
   
        [HttpPost]
        [CustomAuthorize("Administrator", "Client")]
        public async Task<IActionResult> MakeReservation()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                JObject reservationObject = JObject.Parse(requestBody);

                ReservationDto reservationDto = new ReservationDto
                {
                    Name = reservationObject["Name"].ToString(),
                    NumberOfPeople = (int)reservationObject["NumberOfPeople"],
                    ReservationDate = reservationObject["ReservationDate"].ToObject<DateTime>(),
                    Time = reservationObject["Time"].ToObject<TimeSpan>(),
                    Message = reservationObject["Message"].ToString(),
                    TableId = (int)reservationObject["TableId"],
                    UserId = (int)reservationObject["UserId"]
                };

                var isSuccess = await _reservationService.MakeReservation(reservationDto);
                if (isSuccess)
                {
                    var order = await _orderService.CurrentOrder();
                    order.TableId = reservationDto.TableId;
                    await _orderService.UpdateOrder(order.Id, order);
                    return RedirectToAction("Payment", "Payment");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        [HttpGet]
        [CustomAuthorize("Administrator", "Client")]
        public async Task<IActionResult> GetAvailableTimes(DateTime reservationDate, int? tableId)
        {
            var availableTime = await _reservationService.GetAvailableTimes(reservationDate, tableId);

            ViewBag.AvailableTime = availableTime;
            return Ok(availableTime);
        }

        [HttpGet]
        [CustomAuthorize("Administrator", "Client")]
        public async Task<IActionResult> GetAvailableTables(DateTime? reservationDate, string time)
        {
            var availableTables = await _reservationService.GetAvailableTables(reservationDate, time);

            ViewBag.AvailableTables = availableTables;
            return Ok(availableTables);
        }

        [CustomAuthorize("Administrator")]
        public async Task<IActionResult> Reservations()
        {
            var reservations = await _reservationService.GetReservations().ToListAsync();
            var availableTables = await _reservationService.GetTables();
            var availableTime = _reservationService.GetTime();

            ViewBag.AvailableTables = availableTables;
            ViewBag.AvailableTime = availableTime;


            return View(reservations);
        }

        [HttpPost]
        [CustomAuthorize("Administrator")]
        public async Task UpdateReservation()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                JObject reservationObject = JObject.Parse(requestBody);

                ReservationDto reservationDto = new ReservationDto
                {
                    ReservationId = reservationObject["ReservationId"].ToObject<int>(),
                    Name = reservationObject["Name"].ToString(),
                    NumberOfPeople = (int)reservationObject["NumberOfPeople"],
                    ReservationDate = reservationObject["ReservationDate"].ToObject<DateTime>(),
                    Time = reservationObject["Time"].ToObject<TimeSpan>(),
                    Message = reservationObject["Message"].ToString(),
                    TableId = (int)reservationObject["TableId"],
                    UserId = (int)reservationObject["UserId"]
                };

              await _reservationService.UpdateReservation(reservationDto);
            }
        }

        [HttpPost]
        [CustomAuthorize("Administrator")]
        public async Task DeleteReservation(int reservationId)
        {
           await _reservationService.DeleteReservation(reservationId);
        }
    }

}
