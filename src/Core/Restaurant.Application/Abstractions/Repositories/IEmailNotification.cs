using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.ViewModels;
using SendGrid;

namespace Restaurant.Application.Abstractions.Repositories
{
    public interface IEmailNotification
    {
        Task<Response> SendBookingEmailAsync(ReservationVM model, bool isReminderEmail = false);
        Task<Response> SendCheckInEmailAsync(DiningTableWithTimeSlotVM model);
    }
}
