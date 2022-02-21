using Bongo.Models.Models;
using Bongo.Models.Models.ViewModels;

namespace Bongo.Services.IServices;

public interface IStudyRoomBookingService
{
    StudyRoomBookingResult BookStudyRoom(StudyRoomBooking? request);
    IEnumerable<StudyRoomBooking> GetAllBooking();
}