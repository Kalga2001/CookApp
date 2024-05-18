using CookApp.BLL.Dtos.QuantityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Dtos.ReservationDtos
{
    public class ReservationWithProducts
    {
        public ReservationDto Reservation { get; set; }
        public List<SelectedProductDto> SelectedProducts { get; set; }
    }
}
