using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sutido.API.ViewModels.Responses
{
    public class BookingResponse
    {
        public long BookingId { get; set; }
        public long ChatRoomId { get; set; }
        public decimal AgreedPricePerSession { get; set; }
        public int SessionsPerWeek { get; set; }
        public string AgreedDays { get; set; } = null!;
        public string AgreedTime { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string BookingStatus { get; set; } = "Pending";
        public DateTimeOffset CreatedAt { get; set; }
    }
}
