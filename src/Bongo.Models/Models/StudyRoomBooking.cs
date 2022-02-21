using System.ComponentModel.DataAnnotations;
using Bongo.Models.Models.ViewModels;

namespace Bongo.Models.Models;

public class StudyRoomBooking : StudyRoomBookingBase
{
    [Key]
    public int BookingId { get; set; }
    public int StudyRoomId { get; set; }
}