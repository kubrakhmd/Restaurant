

namespace Restaurant.Application.ViewModels
{
    public  class DiningTableWithTimeSlotVM
    {
        public int BranchId { get; set; }
        public DateTime ReservationDay { get; set; }
        public string? TableName { get; set; }
        public int Capacity { get; set; }
        public string MealType { get; set; } = null!;
        public string TableStatus { get; set; } = null!;
        public int TimeSlotId { get; set; }
        public string? UserEmailId { get; set; }
    }
}
