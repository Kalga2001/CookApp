﻿using CookApp.BLL.Dtos.ReservationDtos;
using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            // Получаем текущую дату
            DateTime currentDate = DateTime.Today;

            // Получаем доступные столы и время для текущей даты
            var availableTables = await _reservationService.GetTables();
            var availableTime = _reservationService.GetTime();

            ViewBag.AvailableTables = availableTables;
            ViewBag.AvailableTime = availableTime;

            return View();
        }

        [HttpPost]
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
        public async Task<IActionResult> GetAvailableTimes(DateTime reservationDate, int? tableId)
        {
            var availableTime = await _reservationService.GetAvailableTimes(reservationDate, tableId);

            ViewBag.AvailableTime = availableTime;
            return Ok(availableTime);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTables(DateTime? reservationDate, string time)
        {
            var availableTables = await _reservationService.GetAvailableTables(reservationDate, time);

            ViewBag.AvailableTables = availableTables;
            return Ok(availableTables);
        }
    }

}
