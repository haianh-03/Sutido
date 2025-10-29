using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sutido.API.ViewModels.Requests
{
    public class BookingRequest
    {
        [Required]
        public long ChatRoomId { get; set; }

        [Required]
        public decimal AgreedPricePerSession { get; set; }

        [Required]
        public int SessionsPerWeek { get; set; }

        [Required]
        public string AgreedDays { get; set; } = null!;

        [Required]
        public string AgreedTime { get; set; } = null!;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        public string SecurityCode { get; set; } = Guid.NewGuid().ToString()[..6];
    }
}