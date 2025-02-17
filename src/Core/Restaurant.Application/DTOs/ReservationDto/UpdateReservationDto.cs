namespace Restaurant.Application.DTOs.ReservationDto { 
    public class UpdateReservationDto
    {
        public int NumberOfGuests { get; set; }
        public string? SpecialRequest { get; set; }
    }
}
