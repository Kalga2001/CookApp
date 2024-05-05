using CookApp.BLL.Dtos.ReservationDtos;
using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CookApp.API.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
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
                    Time = reservationObject["Time"].ToObject<DateTime>(),
                    Message = reservationObject["Message"].ToString(),
                    TableId = (int)reservationObject["TableId"],
                    UserId = (int)reservationObject["UserId"]
                };

                var isSuccess = await _reservationService.MakeReservation(reservationDto);
                if (isSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
        }
    }
}
